using UnityEngine.Events;

namespace CrazyShooter.UI
{
    public interface IScrollDataItem<T>
    {
        public void Setup(T data, UnityAction onButtonClick);
        public void SetAsSelected(bool selected);
    }
}