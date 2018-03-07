// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using UnityEngine;

namespace SimpleGUI.Fields
{
    /// <summary>
    /// Content that is used in the hero kit editor.
    /// </summary>
    internal static class Content
    {
        /// <summary>
        /// Initialize content styles.
        /// </summary>
        static Content()
        {
            Set_ContentDefault();
            Set_HeroObjectIcon();
            Set_HeroActionIcon();
            Set_HeroPropertyIcon();

            Set_MenuIcon();
            Set_MoveUpIcon();
            Set_MoveDownIcon();
            Set_AddIcon();
            Set_CopyIcon();
            Set_DeleteIcon();
            Set_PasteIcon();
            Set_RestoreIcon();
        }

        /// <summary>
        /// Default style for content in GUI objects.
        /// </summary>      
        public static GUIContent ContentDefault { get { return contentDefault; } }
        private static GUIContent contentDefault;
        private static void Set_ContentDefault()
        {
            contentDefault = new GUIContent();
        }

        //--------------------------------------
        // Title Window
        //--------------------------------------

        /// <summary>
        /// The Hero Object icon that appears in the title window.
        /// </summary>       
        public static GUIContent HeroObjectIcon { get { return heroObjectIcon; } }
        private static GUIContent heroObjectIcon;
        public static void Set_HeroObjectIcon()
        {
            string icon = "iVBORw0KGgoAAAANSUhEUgAAAB4AAAAdCAYAAAC9pNwMAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAUBJREFUeNrEljsKwkAQhjdDwAvYS3pvogfQwiPYWahoMibgATyAjY1Y2XmHXENBe0HQyo0QWTWPmXGjAwMJm+TbfzKPdQ7HkxJapN3RPpG87AqhUwN40x5yPwBCqAmaaferBk9y1LHhwFQaFayz4CAMbxE8sAWeMpMHKXCwDCXDXQp0s43VYhc/F1bjjvIa9ed9d7BU+/Plcd1uemrYb6XwNPxkxb5QaZZypIL9vF0KLciCQ8ZDNqG5cCjbWVVw1wgvGdqbr7+BP/59Ap5rH6nfWQKvgfqTJYrH2q+cUBfVMcGSMg3AKHL8gdAw/c/w1uCrhL8MEMhYxIqUYlnnIo82idLUnILDno3WOeP06o9EsA2lzGMpvDRXgFMCtqCcM1dIPMghtSo4LTMqgSMnGbm9Og+O3AqQDIl3OErK7i7AAA3qUSE8iXxsAAAAAElFTkSuQmCC";
            heroObjectIcon = new GUIContent();
            heroObjectIcon.image = SimpleGUICommon.StringToTexture(icon);
        }

        /// <summary>
        /// The Hero Action icon that appears in the title window.
        /// </summary>       
        public static GUIContent HeroActionIcon { get { return heroActionIcon; } }
        private static GUIContent heroActionIcon;
        public static void Set_HeroActionIcon()
        {
            string icon = "iVBORw0KGgoAAAANSUhEUgAAAB4AAAAdCAYAAAC9pNwMAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyBpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMC1jMDYwIDYxLjEzNDc3NywgMjAxMC8wMi8xMi0xNzozMjowMCAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENTNSBXaW5kb3dzIiB4bXBNTTpJbnN0YW5jZUlEPSJ4bXAuaWlkOjRFRTg4REYxNDI2OTExRTc4Njc3OTc4OERERkUyNzAxIiB4bXBNTTpEb2N1bWVudElEPSJ4bXAuZGlkOjRFRTg4REYyNDI2OTExRTc4Njc3OTc4OERERkUyNzAxIj4gPHhtcE1NOkRlcml2ZWRGcm9tIHN0UmVmOmluc3RhbmNlSUQ9InhtcC5paWQ6NEVFODhERUY0MjY5MTFFNzg2Nzc5Nzg4RERGRTI3MDEiIHN0UmVmOmRvY3VtZW50SUQ9InhtcC5kaWQ6NEVFODhERjA0MjY5MTFFNzg2Nzc5Nzg4RERGRTI3MDEiLz4gPC9yZGY6RGVzY3JpcHRpb24+IDwvcmRmOlJERj4gPC94OnhtcG1ldGE+IDw/eHBhY2tldCBlbmQ9InIiPz5O2ZzdAAACcklEQVR42rSXX08TQRDA57a0BFJMNNAH/xKCEXgiJhaMPuojXwBNeNPvYKC90kSf9AMQxc+gj4TE2oAPlvaBSI2U9CC5VlCCUhJSaO/O3cUuZ9vd273USSaZvT/7u5nO7Ew18/su+JQkVg3rrJ+Xu3xC51zAU6zzqhsgn1A3KIE19r/BsxzvlOFI0dOk4L4SHPkMrwge7xR4TjF5dBk46jBUGt6lCjXX1mDl1UuwTmvs2ujUFNyemWkHb4Rf2uMYz9Ni6gP82t6GSrnE1Eh/FHmuy4JjvK88qVTgR/4rtbvDYQhhJXJgGLC38YUHj7eDozYPJXg77G8VYH/zG7UHxsbg+uRdaterVdhZ/ST6SVvgyOvL3FLK5sCxbWpfvDEIww8fgIbOtthZXQHbsqThyBVeIdSu1cBIpahNYJHRERi4NQLBnp6zaBQKgAIBr2xncAJ+LgpvQ37iEB8YRWqHenvhWnQCwpEIXL0TZc9k3y7KlBqBv5DuTlvLy+fe43Dn37+jXbF6+JtdN9JpGH/0GAKhkFRbfEYSVhRqkjylbJata8fH8HlhoeW5SrksAyVlGkeuIueC9/IbcGianl6cHB1B5s1rT2jzyZVoOnGYlHM56jUR4tH49DR09/XRteM4UFhawmW2Sde76+uiBqLzjsy28OLfbCZyaWgIok+e/pvxdYuBzUyG56nudVYTuOPO9EAwCBcuX6H24L37xE2cVxp7of/mMLtvW3Whpw3RBMNeTKbMJPqz9Fndkgidhsr0Y7/whNdJKDOBqMI9oSoz17zkIKfLQFWnzKQHXFdJRtW5mgfXVSvAzz+JZrjup+z+CDAAWsHUC7TPsRcAAAAASUVORK5CYII=";
            heroActionIcon = new GUIContent();
            heroActionIcon.image = SimpleGUICommon.StringToTexture(icon);
        }

        /// <summary>
        /// The Hero Property icon that appears in the title window.
        /// </summary>       
        public static GUIContent HeroPropertyIcon { get { return heroPropertyIcon; } }
        private static GUIContent heroPropertyIcon;
        public static void Set_HeroPropertyIcon()
        {
            string icon = "iVBORw0KGgoAAAANSUhEUgAAAB4AAAAdCAYAAAC9pNwMAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAA2RpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMC1jMDYwIDYxLjEzNDc3NywgMjAxMC8wMi8xMi0xNzozMjowMCAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtcE1NOk9yaWdpbmFsRG9jdW1lbnRJRD0ieG1wLmRpZDoyODhGMkE2MzY5NDJFNzExQjczQ0VBNDU5OTgxRDYzNSIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDpFOEE0MDNCNTQyNjkxMUU3QUMzRThFMzdGNUY1RDFDNyIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpFOEE0MDNCNDQyNjkxMUU3QUMzRThFMzdGNUY1RDFDNyIgeG1wOkNyZWF0b3JUb29sPSJBZG9iZSBQaG90b3Nob3AgQ1M1IFdpbmRvd3MiPiA8eG1wTU06RGVyaXZlZEZyb20gc3RSZWY6aW5zdGFuY2VJRD0ieG1wLmlpZDoyODhGMkE2MzY5NDJFNzExQjczQ0VBNDU5OTgxRDYzNSIgc3RSZWY6ZG9jdW1lbnRJRD0ieG1wLmRpZDoyODhGMkE2MzY5NDJFNzExQjczQ0VBNDU5OTgxRDYzNSIvPiA8L3JkZjpEZXNjcmlwdGlvbj4gPC9yZGY6UkRGPiA8L3g6eG1wbWV0YT4gPD94cGFja2V0IGVuZD0iciI/Pu/6hokAAAHxSURBVHjaYnzy/AUDmaAZiBmBuIYczSxkWlqLZOEvIG4i1QAmMi1FtqgRiOtobXENDt+RbDkTiT5txiNPkuVMZAYvPsvrqWVxLYmJp4EYy5mobCnRlrOQYumkwzMYDt07hqFQWViRwV7ZmsFVzYmBlZkF2XJY8BNtcR02DW++vmV4+fkVhmKQ2PEHpxiefHjKkGgWi245I5Ij8AZ1HS5XsjKz4gye/0C44sI6hvNPL6JL1RNjcT0uS9GBjoQmw4SADoZC+2wGDhZ2sNjvv78Zrry4jk05huVMhFyGC0jwiTMYyxgwOKrYMghwCcDFQZbjACjmsyAFbwMpyfb+24cMay9tAgfti08v4eJiPKL4tMFSegPI4jYgriQ1v9x9e59hwqFpKGLC3EIMBtK6hLSCLGcnt3bCAKLcwgxFDjkMikLyRFeLVUD8k9SgFuURYdCX0mFgYmRiUBFRYnBUtgXHOxEAVDbUs6BlcqIt1wam6nq3ClIDpgkWz0xoBTzRFv8Bpl48KZhgBcKERbKBgfqgCV8+Jli1/fzzC87+8ecnWT6FAUY8jb06YksxApY2kFotNhFbqZNqKTH1MbmWE0wrTKRkAWpZSkqbq4nIhlwDsbmClFZmMwHLG0hJjKS2q3FZ3kBqDiCnJ4FueQM52Q4gwABRZYZmrSdDvAAAAABJRU5ErkJggg==";
            heroPropertyIcon = new GUIContent();
            heroPropertyIcon.image = SimpleGUICommon.StringToTexture(icon);
        }

        //--------------------------------------
        // Visual Buttons (these have icons)
        //--------------------------------------

        /// <summary>
        /// [menu icon] show or close a menu.
        /// </summary>
        public static GUIContent MenuIcon { get { return menuIcon; } }
        private static GUIContent menuIcon;
        private static void Set_MenuIcon()
        {
            string icon = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAABmJLR0QA/wD/AP+gvaeTAAAAKklEQVQ4jWNgGDZgPwMDw38S8V4GBgYGJqgBLGRYSo4eGoLRMBgNA4oAAAe1KG+zgbMAAAAAAElFTkSuQmCC";
            menuIcon = new GUIContent();
            menuIcon.image = SimpleGUICommon.StringToTexture(icon);
            menuIcon.tooltip = "View Settings for HeroKit";
        }

        /// <summary>
        /// [move up icon] move item up in list.
        /// </summary>
        public static GUIContent MoveUpIcon { get { return moveUpIcon; } }
        private static GUIContent moveUpIcon;
        private static void Set_MoveUpIcon()
        {
            string icon = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAMAAAAoLQ9TAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyBpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMC1jMDYwIDYxLjEzNDc3NywgMjAxMC8wMi8xMi0xNzozMjowMCAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENTNSBXaW5kb3dzIiB4bXBNTTpJbnN0YW5jZUlEPSJ4bXAuaWlkOjVFODQzRTU5NDU2RjExRTdCOUQ3RUNBOEY5MTJDMDM0IiB4bXBNTTpEb2N1bWVudElEPSJ4bXAuZGlkOjVFODQzRTVBNDU2RjExRTdCOUQ3RUNBOEY5MTJDMDM0Ij4gPHhtcE1NOkRlcml2ZWRGcm9tIHN0UmVmOmluc3RhbmNlSUQ9InhtcC5paWQ6NUU4NDNFNTc0NTZGMTFFN0I5RDdFQ0E4RjkxMkMwMzQiIHN0UmVmOmRvY3VtZW50SUQ9InhtcC5kaWQ6NUU4NDNFNTg0NTZGMTFFN0I5RDdFQ0E4RjkxMkMwMzQiLz4gPC9yZGY6RGVzY3JpcHRpb24+IDwvcmRmOlJERj4gPC94OnhtcG1ldGE+IDw/eHBhY2tldCBlbmQ9InIiPz5bzvlkAAADAFBMVEX29vaRkZFbW1tFRUUyMjITExMAAAD///8ICAgJCQkKCgoLCwsMDAwNDQ0ODg4PDw8QEBARERESEhITExMUFBQVFRUWFhYXFxcYGBgZGRkaGhobGxscHBwdHR0eHh4fHx8gICAhISEiIiIjIyMkJCQlJSUmJiYnJycoKCgpKSkqKiorKyssLCwtLS0uLi4vLy8wMDAxMTEyMjIzMzM0NDQ1NTU2NjY3Nzc4ODg5OTk6Ojo7Ozs8PDw9PT0+Pj4/Pz9AQEBBQUFCQkJDQ0NERERFRUVGRkZHR0dISEhJSUlKSkpLS0tMTExNTU1OTk5PT09QUFBRUVFSUlJTU1NUVFRVVVVWVlZXV1dYWFhZWVlaWlpbW1tcXFxdXV1eXl5fX19gYGBhYWFiYmJjY2NkZGRlZWVmZmZnZ2doaGhpaWlqampra2tsbGxtbW1ubm5vb29wcHBxcXFycnJzc3N0dHR1dXV2dnZ3d3d4eHh5eXl6enp7e3t8fHx9fX1+fn5/f3+AgICBgYGCgoKDg4OEhISFhYWGhoaHh4eIiIiJiYmKioqLi4uMjIyNjY2Ojo6Pj4+QkJCRkZGSkpKTk5OUlJSVlZWWlpaXl5eYmJiZmZmampqbm5ucnJydnZ2enp6fn5+goKChoaGioqKjo6OkpKSlpaWmpqanp6eoqKipqamqqqqrq6usrKytra2urq6vr6+wsLCxsbGysrKzs7O0tLS1tbW2tra3t7e4uLi5ubm6urq7u7u8vLy9vb2+vr6/v7/AwMDBwcHCwsLDw8PExMTFxcXGxsbHx8fIyMjJycnKysrLy8vMzMzNzc3Ozs7Pz8/Q0NDR0dHS0tLT09PU1NTV1dXW1tbX19fY2NjZ2dna2trb29vc3Nzd3d3e3t7f39/g4ODh4eHi4uLj4+Pk5OTl5eXm5ubn5+fo6Ojp6enq6urr6+vs7Ozt7e3u7u7v7+/w8PDx8fHy8vLz8/P09PT19fX29vb39/f4+Pj5+fn6+vr7+/v8/Pz9/f3+/v7////NwWYUAAAACHRSTlP/////////AN6DvVkAAAAwSURBVHjaYmBHAwxUFBAWRhUQZmMTRhYA8mEiDHA+VIQBwYeIgAXggMouhQOAAAMAHuYH3RCYXkEAAAAASUVORK5CYII=";
            moveUpIcon = new GUIContent();
            moveUpIcon.image = SimpleGUICommon.StringToTexture(icon);
            moveUpIcon.tooltip = "Move this item up.";
        }

        /// <summary>
        /// [move down icon] move item down in list.
        /// </summary>
        public static GUIContent MoveDownIcon { get { return moveDownIcon; } }
        private static GUIContent moveDownIcon;
        private static void Set_MoveDownIcon()
        {
            string icon = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAMAAAAoLQ9TAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyBpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMC1jMDYwIDYxLjEzNDc3NywgMjAxMC8wMi8xMi0xNzozMjowMCAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENTNSBXaW5kb3dzIiB4bXBNTTpJbnN0YW5jZUlEPSJ4bXAuaWlkOjE3RDcyMkMzNDU2RjExRTdBRThBODJBNTE1RTMwRjI0IiB4bXBNTTpEb2N1bWVudElEPSJ4bXAuZGlkOjE3RDcyMkM0NDU2RjExRTdBRThBODJBNTE1RTMwRjI0Ij4gPHhtcE1NOkRlcml2ZWRGcm9tIHN0UmVmOmluc3RhbmNlSUQ9InhtcC5paWQ6MTdENzIyQzE0NTZGMTFFN0FFOEE4MkE1MTVFMzBGMjQiIHN0UmVmOmRvY3VtZW50SUQ9InhtcC5kaWQ6MTdENzIyQzI0NTZGMTFFN0FFOEE4MkE1MTVFMzBGMjQiLz4gPC9yZGY6RGVzY3JpcHRpb24+IDwvcmRmOlJERj4gPC94OnhtcG1ldGE+IDw/eHBhY2tldCBlbmQ9InIiPz7bdJAIAAADAFBMVEX29vaRkZFbW1tFRUUyMjITExMAAAD///8ICAgJCQkKCgoLCwsMDAwNDQ0ODg4PDw8QEBARERESEhITExMUFBQVFRUWFhYXFxcYGBgZGRkaGhobGxscHBwdHR0eHh4fHx8gICAhISEiIiIjIyMkJCQlJSUmJiYnJycoKCgpKSkqKiorKyssLCwtLS0uLi4vLy8wMDAxMTEyMjIzMzM0NDQ1NTU2NjY3Nzc4ODg5OTk6Ojo7Ozs8PDw9PT0+Pj4/Pz9AQEBBQUFCQkJDQ0NERERFRUVGRkZHR0dISEhJSUlKSkpLS0tMTExNTU1OTk5PT09QUFBRUVFSUlJTU1NUVFRVVVVWVlZXV1dYWFhZWVlaWlpbW1tcXFxdXV1eXl5fX19gYGBhYWFiYmJjY2NkZGRlZWVmZmZnZ2doaGhpaWlqampra2tsbGxtbW1ubm5vb29wcHBxcXFycnJzc3N0dHR1dXV2dnZ3d3d4eHh5eXl6enp7e3t8fHx9fX1+fn5/f3+AgICBgYGCgoKDg4OEhISFhYWGhoaHh4eIiIiJiYmKioqLi4uMjIyNjY2Ojo6Pj4+QkJCRkZGSkpKTk5OUlJSVlZWWlpaXl5eYmJiZmZmampqbm5ucnJydnZ2enp6fn5+goKChoaGioqKjo6OkpKSlpaWmpqanp6eoqKipqamqqqqrq6usrKytra2urq6vr6+wsLCxsbGysrKzs7O0tLS1tbW2tra3t7e4uLi5ubm6urq7u7u8vLy9vb2+vr6/v7/AwMDBwcHCwsLDw8PExMTFxcXGxsbHx8fIyMjJycnKysrLy8vMzMzNzc3Ozs7Pz8/Q0NDR0dHS0tLT09PU1NTV1dXW1tbX19fY2NjZ2dna2trb29vc3Nzd3d3e3t7f39/g4ODh4eHi4uLj4+Pk5OTl5eXm5ubn5+fo6Ojp6enq6urr6+vs7Ozt7e3u7u7v7+/w8PDx8fHy8vLz8/P09PT19fX29vb39/f4+Pj5+fn6+vr7+/v8/Pz9/f3+/v7////NwWYUAAAACHRSTlP/////////AN6DvVkAAAAvSURBVHjaYmBHAwxUEhCGA5gAGxTABGAiwggzhOF8mKHCMD7cFmFhKrsUBQAEGAA9cgfdV8HjNgAAAABJRU5ErkJggg==";
            moveDownIcon = new GUIContent();
            moveDownIcon.image = SimpleGUICommon.StringToTexture(icon);
            moveDownIcon.tooltip = "Move this item down.";
        }

        /// <summary>
        /// [add icon] add item to list.
        /// </summary>
        public static GUIContent AddIcon { get { return addIcon; } }
        private static GUIContent addIcon;
        private static void Set_AddIcon()
        {
            string icon = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAMAAAAoLQ9TAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAADBQTFRFBUMFtLS0xsbG/v7+7e3tsLCw8PDw9PT09fX1r6+v+vr6AF0A//////8AAAAA////cCrdNwAAABB0Uk5T////////////////////AOAjXRkAAAB0SURBVHjalI8xEgQhCAQRF1dE5f+/vUEM7sLrgKBnaqogp/2FO/l+Lq21dx/Bqly44PYUyF7QwRENDRUVER5HRDaS+t+GgZ8NW8tyo7PIg3hFJ8SZR75MVwqWUjLPRq11TqLIr5hgzOhQfOv3c3TM3T8CDADtpwmmvA50ugAAAABJRU5ErkJggg==";
            addIcon = new GUIContent();
            addIcon.image = SimpleGUICommon.StringToTexture(icon);
            addIcon.tooltip = "Create a new item below this item.";
        }

        /// <summary>
        /// [copy icon] copy item in list.
        /// </summary>
        public static GUIContent CopyIcon { get { return copyIcon; } }
        private static GUIContent copyIcon;
        private static void Set_CopyIcon()
        {
            string icon = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAMAAAAoLQ9TAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAADBQTFRF1NTUtLS02traxsbGsLCw6urq7e3tr6+v8PDw/v7+9fX19PT0+vr6////AAAA////U/ddPAAAABB0Uk5T////////////////////AOAjXRkAAAB6SURBVHjaTNBRDsQgCARQESwiCve/7SJZbefPRzIQi7ud+E5xa5Hn6V3tQGFGYBwHcqpjzAO92acmQDXs1gSMag1vTcCct+YPaD0WMRHOBBHLJRFJWMtGIapImLBPz6lIjDb4riEA0FUP5HQp8oV4rgmcl6a8H/ATYADPIQsWeC2Y+wAAAABJRU5ErkJggg==";
            copyIcon = new GUIContent();
            copyIcon.image = SimpleGUICommon.StringToTexture(icon);
            copyIcon.tooltip = "Copy this item.";
        }

        /// <summary>
        /// [paste icon] paste item in list.
        /// </summary>
        public static GUIContent PasteIcon { get { return pasteIcon; } }
        private static GUIContent pasteIcon;
        private static void Set_PasteIcon()
        {
            string icon = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAMAAAAoLQ9TAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAADBQTFRFilYj3LmIeUMXnJiT5cOP+Pj40q5/o3I0m2kvd3d3cz0TyKN4sLCw6enp////////MaT6uQAAABB0Uk5T////////////////////AOAjXRkAAABsSURBVHjaVM5REoAgCEVRC0EtwP3vNhAzuz/mmTdOqXsMAMzjM3UqdlNEtKOQQZGGKBZik+KQuc04B+hqQM0qM801gK0d+n+RjrHw9EgBaxFA70KUJqzFgNMWMt+g0+H+fuwO2HKAawv6I8AAi60KkM5e2GsAAAAASUVORK5CYII=";
            pasteIcon = new GUIContent();
            pasteIcon.image = SimpleGUICommon.StringToTexture(icon);
            pasteIcon.tooltip = "Paste an item you copied below this item.";
        }

        /// <summary>
        /// [delete icon] delete item in list.
        /// </summary>
        public static GUIContent DeleteIcon { get { return deleteIcon; } }
        private static GUIContent deleteIcon;
        private static void Set_DeleteIcon()
        {
            string icon = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAMAAAAoLQ9TAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAADBQTFRF////yikAkwAA/8z/mqSwxMvU/2wUucTPKjVA1tzisLvH8PP04+rwbHaCh5Gb////upeMdgAAABB0Uk5T////////////////////AOAjXRkAAAB4SURBVHjaTI8LDsMwCEMN+S0lNPe/bU2ydXmSJXgSlsAMnMwN1m7Ef8IPQjj+dA9ho/dS2nXlfC/hPZObbDEdxUbuLV/N5yEGMMohDCJgXgFJSSKv0FpTqlUpbJ/QcD87QqwO+3YI2R2ADaiIRvjtJ2ArZ9X5CDAAJH8IPyIdS2gAAAAASUVORK5CYII=";
            deleteIcon = new GUIContent();
            deleteIcon.image = SimpleGUICommon.StringToTexture(icon);
            deleteIcon.tooltip = "Delete this item.";
        }

        /// <summary>
        /// [restore icon] restore item in list.
        /// </summary>
        public static GUIContent RestoreIcon { get { return restoreIcon; } }
        private static GUIContent restoreIcon;
        private static void Set_RestoreIcon()
        {
            string icon = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAMAAAAoLQ9TAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyBpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMC1jMDYwIDYxLjEzNDc3NywgMjAxMC8wMi8xMi0xNzozMjowMCAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENTNSBXaW5kb3dzIiB4bXBNTTpJbnN0YW5jZUlEPSJ4bXAuaWlkOjUwMzkyQzNBNDU3MTExRTdCRDBDQTY1MTVGNjY3RTM0IiB4bXBNTTpEb2N1bWVudElEPSJ4bXAuZGlkOjUwMzkyQzNCNDU3MTExRTdCRDBDQTY1MTVGNjY3RTM0Ij4gPHhtcE1NOkRlcml2ZWRGcm9tIHN0UmVmOmluc3RhbmNlSUQ9InhtcC5paWQ6NTAzOTJDMzg0NTcxMTFFN0JEMENBNjUxNUY2NjdFMzQiIHN0UmVmOmRvY3VtZW50SUQ9InhtcC5kaWQ6NTAzOTJDMzk0NTcxMTFFN0JEMENBNjUxNUY2NjdFMzQiLz4gPC9yZGY6RGVzY3JpcHRpb24+IDwvcmRmOlJERj4gPC94OnhtcG1ldGE+IDw/eHBhY2tldCBlbmQ9InIiPz61+F6tAAAAeFBMVEUAAACm1p2s2aOWzY2g05ij1Zqo15+WzoyTzIuMxoOp2KB7uXOe0ZV1tG2a0JF0tWuFxX1qrmOQy4mGxX+u2qSw26er2aKNyYZusWVxsGpdolav26ZhpVmNyYWLyIWBv3mx3KeIx4Kt2aOTy4pprWFemVlvsmb////SMO/QAAAAKHRSTlP///////////////////////////////////////////////////8AvqouGAAAAGZJREFUeNp8j8kSQ1AQRQ9iiCkxJmaCvP//QwtUaaXc5anuO6BO4gqwawP0Waq7mv98sYHql1ua+bBDD/liOEhT3gGKQxRJtwIKxAX/SHjwHePPMYWpaQfZoy5lU8XMaRx3axcBBgBp0xmuZUKsfgAAAABJRU5ErkJggg==";
            restoreIcon = new GUIContent();
            restoreIcon.image = SimpleGUICommon.StringToTexture(icon);
            restoreIcon.tooltip = "Restore last deleted item.";
        }
    }
}










