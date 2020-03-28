namespace VpNet.Interfaces
{
    public interface IChatFunctions<in TAvatar> 
        where TAvatar : class, IAvatar, new()
        
    {
        void Say(string message);
        void Say(string format, params object[] arg);

        void ConsoleMessage(TAvatar targetAvatar, string name, string message, TextEffectTypes effects = 0, byte red = 0, byte green = 0, byte blue = 0);
        void ConsoleMessage(int targetSession, string name, string message, TextEffectTypes effects = 0, byte red = 0, byte green = 0, byte blue = 0);
        void ConsoleMessage(TAvatar targetAvatar, string name, string message, Color color, TextEffectTypes effects = 0);
        void ConsoleMessage(int targetSession, string name, string message, Color color, TextEffectTypes effects = 0);
        void ConsoleMessage(string name, string message, Color color, TextEffectTypes effects = 0);
        void ConsoleMessage(string message, Color color, TextEffectTypes effects = 0);
        void ConsoleMessage(string message);
    }
}
