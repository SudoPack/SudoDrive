using Microsoft.AspNetCore.Mvc;
using Server.Exceptions;
using Server.Libraries;
using Server.Middlewares;
using Server.Models.VO;
using Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers.GroupManage
{
    [Route("api/groupmanage")]
    [ApiController]
    [NeedPermission(PermissionBank.GroupmanageDeletegroup)]
    public class DeleteGroupController : AbstractController
    {
        private IDatabaseService _databaseService;
        public DeleteGroupController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpDelete]
        public IActionResult DeleteGroup([FromBody] DeleteGroupRequestModel deleteGroupRequestModel)
        {
            //use groupname to identify group,because the id is invisible to user
            var group_db = _databaseService.Groups.FirstOrDefault(t => t.GroupName == deleteGroupRequestModel.GroupName);
            if (group_db == null)
            {
                throw new GroupnameNotExistException("Groupname Does Not Exist when trying to delete group.");
            }
           
            var grouptouser_db = _databaseService.GroupsToUsersRelation.Where(t => t.Group.GroupName == group_db.GroupName);
            //try to update the updatetime of all the users which belongs to this group, but failed as below
            //var user_db = _databaseService.Users.Where(t => grouptouser_db.Contains(t.GroupToUser));
            var grouptopermission_db = _databaseService.GroupsToPermissionsRelation.Where(t => t.Group.GroupName == group_db.GroupName);
            _databaseService.GroupsToPermissionsRelation.RemoveRange(grouptopermission_db);
            _databaseService.GroupsToUsersRelation.RemoveRange(grouptouser_db);
            _databaseService.Groups.Remove(group_db);
            _databaseService.SaveChanges();
            return Ok();
        }
    }
}
