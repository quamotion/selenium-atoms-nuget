using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Selenium.Atoms
{
    public static class Atom
    {
        private static readonly Dictionary<AtomTarget, string> targetNames;
        private static readonly Dictionary<AtomType, string> typeNames;

        static Atom()
        {
            targetNames = new Dictionary<AtomTarget, string>();
            targetNames.Add(AtomTarget.Android, "_android");
            targetNames.Add(AtomTarget.Chrome, "_chrome");
            targetNames.Add(AtomTarget.FireFox, "_firefox");
            targetNames.Add(AtomTarget.InternetExplorer, "_ie");
            targetNames.Add(AtomTarget.iOS, "_ios");
            targetNames.Add(AtomTarget.Default, string.Empty);

            typeNames = new Dictionary<AtomType, string>();
            typeNames.Add(AtomType.ActiveElement, "active_element");
            typeNames.Add(AtomType.Clear, "clear");
            typeNames.Add(AtomType.ClearLocalStorage, "clear_local_storage");
            typeNames.Add(AtomType.ClearSessionStorage, "clear_session_storage");
            typeNames.Add(AtomType.Click, "click");
            typeNames.Add(AtomType.DefaultContent, "default_content");
            typeNames.Add(AtomType.ExecuteAsyncScript, "execute_async_script");
            typeNames.Add(AtomType.ExecuteScript, "execute_script");
            typeNames.Add(AtomType.ExecuteSql, "execute_sql");
            typeNames.Add(AtomType.FindElement, "find_element");
            typeNames.Add(AtomType.FindElements, "find_elements");
            typeNames.Add(AtomType.FrameByIdOrName, "frame_by_id_or_name");
            typeNames.Add(AtomType.FrameByIndex, "frame_by_index");
            typeNames.Add(AtomType.GetAppcacheStatus, "get_appcache_status");
            typeNames.Add(AtomType.GetAttributeValue, "get_attribute_value");
            typeNames.Add(AtomType.GetFrameWinow, "get_frame_window");
            typeNames.Add(AtomType.GetLocalStorageItem, "get_local_storage_item");
            typeNames.Add(AtomType.GetLocalStorageKeys, "get_local_storage_keys");
            typeNames.Add(AtomType.GetLocalStorageSize, "get_local_storage_size");
            typeNames.Add(AtomType.GetParentFrame, "get_parent_frame");
            typeNames.Add(AtomType.GetSessionStorageItem, "get_session_storage_item");
            typeNames.Add(AtomType.GetSessionStorageKeys, "get_session_storage_keys");
            typeNames.Add(AtomType.GetSessionStorageSize, "get_session_storage_size");
            typeNames.Add(AtomType.GetSize, "get_size");
            typeNames.Add(AtomType.GetText, "get_text");
            typeNames.Add(AtomType.GetTopLeftCoordinates, "get_top_left_coordinates");
            typeNames.Add(AtomType.GetValueOfCssProperty, "get_value_of_css_property");
            typeNames.Add(AtomType.IsDisplayed, "is_displayed");
            typeNames.Add(AtomType.IsEnabled, "is_enabled");
            typeNames.Add(AtomType.IsSelected, "is_selected");
            typeNames.Add(AtomType.MouseClick, "mouse_click");
            typeNames.Add(AtomType.MouseDoubleClick, "mouse_double_click");
            typeNames.Add(AtomType.MouseDown, "mouse_down");
            typeNames.Add(AtomType.MouseMove, "mouse_move");
            typeNames.Add(AtomType.MouseUp, "mouse_up");
            typeNames.Add(AtomType.RemoveLocalStorageItem, "remove_local_storage_item");
            typeNames.Add(AtomType.RemoveSessionStorageItem, "remove_session_storage_item");
            typeNames.Add(AtomType.SendKeysToActiveElement, "send_keys_to_active_element");
            typeNames.Add(AtomType.SetLocalStorageItem, "set_local_storage_item");
            typeNames.Add(AtomType.SetSessionStorageItem, "set_session_storage_item");
            typeNames.Add(AtomType.Submit, "submit");
            typeNames.Add(AtomType.Type, "type");
        }

        public static string GetInjectableAtom(string atom)
        {
            return GetInjectableAtom(atom, string.Empty);
        }

        public static string GetInjectableAtom(string atom, AtomTarget target)
        {
            if (!targetNames.ContainsKey(target))
            {
                throw new ArgumentOutOfRangeException(nameof(target));
            }

            return GetInjectableAtom(atom, targetNames[target]);
        }

        public static string GetInjectableAtom(AtomType atom)
        {
            if (!typeNames.ContainsKey(atom))
            {
                throw new ArgumentOutOfRangeException(nameof(atom));
            }

            return GetInjectableAtom(typeNames[atom], string.Empty);
        }

        public static string GetInjectableAtom(AtomType atom, AtomTarget target)
        {
            if (!targetNames.ContainsKey(target))
            {
                throw new ArgumentOutOfRangeException(nameof(target));
            }

            if (!typeNames.ContainsKey(atom))
            {
                throw new ArgumentOutOfRangeException(nameof(atom));
            }

            return GetInjectableAtom(typeNames[atom], targetNames[target]);
        }

        internal static string GetInjectableAtom(string atom, string target)
        {
            string name = $"selenium-atoms.{atom}{target}.js";

            var maybeStream = typeof(Atom).GetTypeInfo().Assembly.GetManifestResourceStream(name);

            if (maybeStream == null)
            {
                throw new AtomNotFoundException($"The atom {atom}{target}.js could not be found.");

            }

            using (Stream stream = maybeStream)
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8, false, 4096, leaveOpen: true))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
