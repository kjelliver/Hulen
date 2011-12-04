using System.Collections.Generic;
using System.Text;
using Hulen.Objects.DTO;
using Hulen.Objects.Enum;
using Hulen.Objects.Mappers.Interfaces;
using Hulen.Objects.Models;

namespace Hulen.Objects.Mappers
{
    public class MapAccessGroup : IMapAccessGroup
    {
        public AccessGroupDTO ToDTO(AccessGroup model)
        {
            return new AccessGroupDTO
                          {
                              Id = model.Id,
                              Name = model.Name,
                              Type = model.Type,
                              Description = model.Description,
                              RolesThatHaveAccess = MapRolesInViewModel(model.RolesThatHaveAccess)
                          };
        }

        public AccessGroup ToViewModel(AccessGroupDTO dto)
        {
            return new AccessGroup
                       {
                           Id = dto.Id,
                           Name = dto.Name,
                           Type = dto.Type,
                           Description = dto.Description,
                           RolesThatHaveAccess = MapRolesInDto(dto.RolesThatHaveAccess)
                       };
        }

        private static List<string> MapRolesInDto(string rolesThatHaveAccess)
        {
            var result = new List<string>();
            var roles = rolesThatHaveAccess.Split(',');
            foreach(var role in roles)
            {
                result.Add(((UserRole)int.Parse(role)).ToString());
            }
            return result;
        }

        private static string MapRolesInViewModel(IEnumerable<string> rolesThatHaveAccess)
        {
            var sb = new StringBuilder("");
            foreach(string role in rolesThatHaveAccess)
            {
                sb.Append((int)System.Enum.Parse(typeof (UserRole), role) + ",");
            }
            if(sb.Length > 0)
                sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }
}
