using System;
using System.Collections.Generic;
using PolyAndCode.UI;
using NotImplementedException = System.NotImplementedException;

namespace CrazyShooter.UI
{
    public class ItemsScrollDataSource<T> : IRecyclableScrollRectDataSource
    {
        public delegate void ItemSelectedDelegate();
        public event ItemSelectedDelegate OnItemSelected;
        private List<T> _itemsList = new List<T>();

        public ItemsScrollDataSource(IEnumerable<T> itemsList)
        {
            _itemsList.AddRange(itemsList);
        }

        public int GetItemCount()
        {
            return _itemsList.Count;
        }

        public void SetCell(ICell cell, int index)
        {
            if (cell is IScrollDataItem<T> item)
            {
                var data = _itemsList[index];
                item.Setup(data, () => OnItemClick());
            }
            else
            {
                throw new Exception(
                    $"Can't cast cell of type {cell.GetType().Name} to IScrollDataItem<{typeof(T).Name}>");
            }
        }
        
        private void OnItemClick()
        {
            OnItemSelected?.Invoke();
        }
    }
}