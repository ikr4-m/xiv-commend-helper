using CommendMe.DataStructure;
using Dalamud.Game.Gui;
using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;

namespace CommendMe.Events
{
    public class ExampleEvents : BaseEvents
    {
        private ChatGui _chat;

        public ExampleEvents(ChatGui chat)
        {
            _chat = chat;
            _chat.ChatMessage += ChatMessage;
        }

        public override void Dispose()
        {
            _chat.ChatMessage -= ChatMessage;
        }

        public void ChatMessage (XivChatType type, uint senderId, ref SeString sender, ref SeString message, ref bool isHandled)
        {
            // Implemented
        }
    }
}