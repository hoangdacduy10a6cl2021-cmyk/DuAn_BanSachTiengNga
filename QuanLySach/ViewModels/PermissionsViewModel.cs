using QuanLySach.Models;

namespace QuanLySach.ViewModels
{
    public class ModulePermissionRow
    {
        public string Module { get; set; } = "";
        public string DisplayName { get; set; } = "";
        public bool CanView { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }

    public class PermissionsViewModel
    {
        public List<Role> AllRoles { get; set; } = new();
        public int SelectedRoleId { get; set; }
        public Role? SelectedRole { get; set; }
        public List<ModulePermissionRow> Rows { get; set; } = new();

        public static readonly (string Key, string Label)[] Modules = new (string, string)[]
        {
            ("Books", "Управление книгами"),
            ("Categories", "Категории"),
            ("Authors", "Авторы"),
            ("Publishers", "Издательства"),
            ("Orders", "Заказы"),
            ("Users", "Пользователи"),
            ("Customers", "Клиенты"),
            ("Promotions", "Промоакции"),
            ("Statistics", "Статистика"),
            ("Settings", "Настройки"),
            ("Roles", "Роли"),
            ("AdminAccounts", "Аккаунты администратора"),
            ("Permissions", "Права доступа"),
            ("ActivityLog", "Журнал действий"),
        };
    }
}
