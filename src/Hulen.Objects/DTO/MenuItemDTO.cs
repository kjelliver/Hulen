using System;

namespace Hulen.Objects.DTO
{
    public class MenuItemDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public int MenuLevel { get; set; }
        public string Parent { get; set; }
        public int SortOrder { get; set; }
    }
}
