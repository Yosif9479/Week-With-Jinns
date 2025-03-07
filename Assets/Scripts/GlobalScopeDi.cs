using Interfaces;
using PlayerScripts;
using UnityEngine;
using VContainer.Unity;
using VContainer;

public class GlobalScopeDi : LifetimeScope
{
	[SerializeField] private Player _playerPrefab;
	
	protected override void Configure(IContainerBuilder builder)
	{
		base.Configure(builder);
		
		builder.Register<PlayerInput>(Lifetime.Singleton);
		builder.Register<PlayerMovement>(Lifetime.Scoped).As<IPlayerMovement>();
		builder.Register<PlayerCameraController>(Lifetime.Scoped).As<IPlayerCameraController>();
		builder.Register<Interactor>(Lifetime.Scoped).As<IInteractor>();
	}

	private void Start()
	{
		Container.Instantiate(_playerPrefab);
	}
}