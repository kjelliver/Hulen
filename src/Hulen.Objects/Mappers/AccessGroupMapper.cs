using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Objects.DTO;
using Hulen.Objects.Enum;
using Hulen.Objects.Mappers.Interfaces;
using Hulen.Objects.ViewModels;

namespace Hulen.Objects.Mappers
{
    public class AccessGroupMapper : IAccessGroupMapper
    {
        public AccessGroupDTO ToDTO(AccessGroupViewModel viewModel)
        {
            return new AccessGroupDTO
                          {
                              Id = viewModel.Id,
                              Name = viewModel.Name,
                              Type = viewModel.Type,
                              Description = viewModel.Description,
                              RolesThatHaveAccess = MapRolesInViewModel(viewModel.RolesThatHaveAccess)
                          };
        }

        public AccessGroupViewModel ToViewModel(AccessGroupDTO dto)
        {
            return new AccessGroupViewModel
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
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }
}
