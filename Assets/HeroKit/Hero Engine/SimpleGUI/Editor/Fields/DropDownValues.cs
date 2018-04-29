// --------------------------------------------------------------
// Copyright (c) 2016-2017 Aveyond Studios. 
// All Rights Reserved.
// --------------------------------------------------------------
using System.Linq;

namespace SimpleGUI.Fields
{
    /// <summary>
    /// Structure for a drop-down list with values and IDs.
    /// </summary>
    public struct DropDownValues
    {
        /// <summary>
        /// Name of the list.
        /// </summary>
        public string name;
        /// <summary>
        /// Name of each item in drop-down list.
        /// </summary>
        public string[] items;
        /// <summary>
        /// ID of each item in drop-down list.
        /// </summary>
        public int[] ids;

        /// <summary>
        /// Set the values for a drop-down list.
        /// </summary>
        /// <param name="name">Title of the drop-down list.</param>
        /// <param name="items">Names of the items in the drop-down list.</param>
        public void setValues(string name, string[] items, bool firstIsNone=false)
        {
            if (!firstIsNone)
            {
                this.name = name;
                this.items = items;

                int count = items.Length;
                ids = new int[count];
                for (int i = 0; i < count; i++)
                {
                    int number = i + 1;
                    ids[i] = number;
                }
            }
            else
            {
                this.name = name;
                this.items = new string[items.Length + 1];
                items.CopyTo(this.items, 1);
                this.items[0] = "None";

                int count = items.Length + 1;
                ids = new int[count];
                for (int i = 0; i < count; i++)
                {
                    int number = i;
                    ids[i] = number;
                }
            }
        }


        /// <summary>
        /// Set the values and IDs for a drop-down list. 
        /// </summary>
        /// <param name="name">Title of the drop-down list.</param>
        /// <param name="items">Names of the items in the drop-down list.</param>
        /// <param name="items">IDs of the items in the drop-down list.</param>
        public void setValues(string name, string[] items, int[] itemIDs, bool firstIsNone=false)
        {
            if (!firstIsNone)
            {
                this.name = name;
                this.items = items;
                this.ids = itemIDs;
            }
            else
            {
                this.name = name;
                string[] firstItem = { "None" };
                this.items = firstItem.Concat(items).ToArray();
                int[] firstID = { 0 };
                this.ids = firstID.Concat(itemIDs).ToArray();
            }
        }

        /// <summary>
        /// Delete the values in a drop-down list.
        /// </summary>
        public void clearValues()
        {
            items = new string[0];
            ids = new int[0];
        }
    }
}