using IllusionPlugin;

namespace BSModUI.VersionChecker.Interfaces
{
    public interface IGithubInfoPlugin : IEnhancedPlugin
    {
        /// <summary>
        /// The Github Author
        /// </summary>
        string GithubAuthor { get; }
        /// <summary>
        /// The Github Project Name
        /// </summary>
        string GithubProjName { get; }
    }
}