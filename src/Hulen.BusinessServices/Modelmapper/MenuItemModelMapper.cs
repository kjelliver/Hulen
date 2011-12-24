using System;
using Hulen.BusinessServices.Modelmapper.Interfaces;
using Hulen.BusinessServices.ServiceModel;
using Hulen.Storage.DTO;

namespace Hulen.BusinessServices.Modelmapper
{
    public class MenuItemModelMapper :IMenuItemModelMapper
    {
        public MenuItemDTO ToDTO(MenuItem model)
        {
            return new MenuItemDTO
                       {
                            Id = model.Id,
                            Name = model.Name,
                            Controller = model.Controller,
                            Action = model.Action,
                            MenuLevel = model.MenuLevel,
                            Parent = model.Parent,
                            SortOrder = model.SortOrder,
                            IsLink = model.IsLink,
                            AccessGroup = model.AccessGroup
                       };
        }

        public MenuItem FromDTO(MenuItemDTO dto)
        {
            return new MenuItem
            {
                Id = dto.Id,
                Name = dto.Name,
                Controller = dto.Controller,
                Action = dto.Action,
                MenuLevel = dto.MenuLevel,
                Parent = dto.Parent,
                SortOrder = dto.SortOrder,
                IsLink = dto.IsLink,
                AccessGroup = dto.AccessGroup
            };
        }
    }
}
