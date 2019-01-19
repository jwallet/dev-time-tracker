using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DevTimeTracker
{
    internal class Menus
    {
        private Dictionary<MenuEnum, EventHandler> _menus;

        internal MenuItem GetResumeMenu { get => Menu.MenuItems.Find(MenuEnum.Resume.ToKey(), false).First(); }
        internal MenuItem GetSuspendMenu { get => Menu.MenuItems.Find(MenuEnum.Suspend.ToKey(), false).First(); }
        internal ContextMenu Menu { get; private set; }

        internal Menus(Dictionary<MenuEnum, EventHandler> dict)
        {
            _menus = dict;
            Menu = new ContextMenu();

            foreach (var menu in _menus)
            {
                var menuItem = new MenuItem(menu.Key.ToString(), menu.Value)
                {
                    Name = menu.Key.ToKey()
                };
                Menu.MenuItems.Add(menuItem);
            }
        }
    }
}
