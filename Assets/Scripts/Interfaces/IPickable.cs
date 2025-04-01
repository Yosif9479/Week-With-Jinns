namespace Interfaces
{
	public interface IPickable
	{
		public void OnPickedUp();
		public void OnDropped();
		public bool CanBePickedUp();
	}
}