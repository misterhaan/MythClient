using au.Applications.MythClient.Recordings.Types;

namespace au.Applications.MythClient.UI.Render {
	/// <summary>
	/// Capable of rendering the info panel for all possible levels.
	/// </summary>
	internal interface IInfoRenderer : IInfoRenderer<IShow>, IInfoRenderer<ISeason>, IInfoRenderer<IEpisode> { }

	/// <summary>
	/// Capable of rendering the info panel for the specified item type.
	/// </summary>
	/// <typeparam name="TItem">Item type this IInfoRenderer can render the info panel for</typeparam>
	internal interface IInfoRenderer<TItem> {
		/// <summary>
		/// Render the info panel for the specified item.
		/// </summary>
		/// <param name="item">Item to render to the info panel</param>
		void Render(TItem item);
	}
}
