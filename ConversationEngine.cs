using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZyphraVirtualAssistant
{
    public class ConversationEngine
    {
        private List<ConversationMessage> conversationHistory = new List<ConversationMessage>();
        private Dictionary<string, string> contextMemory = new Dictionary<string, string>();
        private Random random = new Random();

        public class ConversationMessage
        {
            public string Sender { get; set; }
            public string Message { get; set; }
            public DateTime Timestamp { get; set; }
            public string Context { get; set; }
        }

        public ConversationEngine()
        {
            InitializeContextMemory();
        }

        private void InitializeContextMemory()
        {
            contextMemory["user_name"] = "User";
            contextMemory["last_topic"] = "";
            contextMemory["mood"] = "neutral";
            contextMemory["conversation_count"] = "0";
        }

        public string GetQuickResponse(string userMessage)
        {
            string lowerMessage = userMessage.ToLower().Trim();

            // QUICK GREETINGS
            if (lowerMessage == "hi" || lowerMessage == "hello" || lowerMessage == "hey")
                return RandomResponse(new[] { "Hey! How's it going?", "Hi there! What's up?", "Hello! Nice to see you!", "Hey! What can I do for you?" });

            if (lowerMessage == "how are you" || lowerMessage == "how are you?")
                return RandomResponse(new[] { "I'm doing great! How about you?", "Fantastic! Thanks for asking!", "Living my best life! 😊", "All systems go! Ready to help!" });

            // QUICK FAREWELLS
            if (lowerMessage == "bye" || lowerMessage == "goodbye" || lowerMessage == "see you")
                return RandomResponse(new[] { "See you later! Take care!", "Goodbye! Have a great day!", "Catch you soon!", "Thanks for chatting! Bye!" });

            if (lowerMessage == "thanks" || lowerMessage == "thank you")
                return RandomResponse(new[] { "You're welcome! Happy to help!", "Anytime! 😊", "My pleasure!", "Glad I could assist!" });

            // QUICK MOOD RESPONSES
            if (lowerMessage.Contains("good") || lowerMessage.Contains("great") || lowerMessage.Contains("awesome"))
                return RandomResponse(new[] { "That's awesome! 🎉", "Love the positive energy!", "Glad to hear that!", "Fantastic! Keep it up!" });

            if (lowerMessage.Contains("bad") || lowerMessage.Contains("sad") || lowerMessage.Contains("tired"))
                return RandomResponse(new[] { "Sorry to hear that. Want to talk about it?", "Hang in there! Things will get better!", "That's rough. How can I help?", "You've got this! 💪" });

            // QUICK QUESTIONS
            if (lowerMessage == "what's up?" || lowerMessage == "what's new?")
                return RandomResponse(new[] { "Just running smoothly! You?", "Nothing much, ready to chat!", "Life's good! What about you?", "Just here waiting for great conversations!" });

            if (lowerMessage == "who are you?" || lowerMessage == "what are you?")
                return "I'm Zyphra, your AI assistant! I can chat about anything, learn online, and help with whatever you need!";

            if (lowerMessage == "what can you do?")
                return "I can: chat naturally, learn online, answer questions, tell jokes, give advice, and much more! Just ask me anything!";

            if (lowerMessage.StartsWith("tell me ") || lowerMessage.StartsWith("explain "))
                return RandomResponse(new[] { 
                    "Sure! That's an interesting topic. Let me look that up for you...", 
                    "Great question! Let me break that down...",
                    "I love explaining things! Here's what I know...",
                    "Absolutely! Let me share what I know about that..."
                });

            // QUICK JOKES
            if (lowerMessage == "tell me a joke" || lowerMessage == "joke" || lowerMessage == "make me laugh")
                return GetQuickJoke();

            // QUICK FACTS
            if (lowerMessage.Contains("fun fact") || lowerMessage == "tell me something interesting")
                return GetRandomFact();

            // AFFIRMATIONS
            if (lowerMessage == "yes" || lowerMessage == "yeah" || lowerMessage == "yep")
                return RandomResponse(new[] { "Great! Let's do this! 💪", "Awesome! Moving forward!", "Perfect! You got it!", "Let's go! 🚀" });

            if (lowerMessage == "no" || lowerMessage == "nope" || lowerMessage == "nah")
                return RandomResponse(new[] { "No problem! What would you prefer?", "That's cool! What else can I help with?", "All good! Anything else?", "No worries! Got any other questions?" });

            // DEFAULT CONVERSATIONAL RESPONSE
            return "That's interesting! Tell me more about that. What's on your mind?";
        }

        public void AddToConversation(string sender, string message)
        {
            conversationHistory.Add(new ConversationMessage
            {
                Sender = sender,
                Message = message,
                Timestamp = DateTime.Now,
                Context = contextMemory["last_topic"]
            });

            // Keep conversation history manageable
            if (conversationHistory.Count > 100)
                conversationHistory = conversationHistory.Skip(10).ToList();
        }

        public string GetConversationContext()
        {
            if (conversationHistory.Count == 0)
                return "Starting new conversation";

            var recentMessages = conversationHistory.TakeLast(5);
            var context = string.Join(" | ", recentMessages.Select(m => $"{m.Sender}: {m.Message}"));
            return context;
        }

        public List<ConversationMessage> GetConversationHistory()
        {
            return conversationHistory;
        }

        public string ContinueConversation(string userMessage)
        {
            // Extract topic from message
            ExtractTopic(userMessage);

            // Generate contextual response based on conversation history
            if (conversationHistory.Count > 0)
            {
                var lastMessage = conversationHistory.Last();
                return GenerateContextualResponse(userMessage, lastMessage.Message);
            }

            return GetQuickResponse(userMessage);
        }

        private void ExtractTopic(string message)
        {
            if (message.Length > 3)
            {
                var words = message.Split(' ');
                if (words.Length > 1)
                    contextMemory["last_topic"] = string.Join(" ", words.Take(3));
            }
        }

        private string GenerateContextualResponse(string currentMessage, string previousMessage)
        {
            string lower = currentMessage.ToLower();

            // Follow-up questions
            if (lower.Contains("why") || lower.Contains("how") || lower.Contains("what"))
            {
                return RandomResponse(new[] {
                    "That's a great follow-up question! Let me think about that...",
                    "Interesting! That's a deeper dive into the topic...",
                    "Good question! Here's what I think...",
                    "I like where you're going with this..."
                });
            }

            // Agreement or disagreement
            if (lower.Contains("i agree") || lower.Contains("i think so"))
                return RandomResponse(new[] { "Right on! We're on the same page!", "Exactly! Glad we agree!", "Yes! That makes sense!" });

            if (lower.Contains("i disagree") || lower.Contains("i don't think"))
                return RandomResponse(new[] { "That's a valid point! Let's explore it...", "Interesting perspective! I see your angle...", "Good counter-argument! Tell me more..." });

            // Personal experiences
            if (lower.Contains("happened") || lower.Contains("i did") || lower.Contains("i went"))
                return RandomResponse(new[] { 
                    "That sounds like quite an experience! Tell me more!",
                    "Wow! What happened next?",
                    "That's cool! How did that make you feel?",
                    "Interesting story! Continue..."
                });

            // Emotions
            if (lower.Contains("feel") || lower.Contains("emotion"))
                return RandomResponse(new[] {
                    "Feelings are important! I'm here to listen.",
                    "It's okay to feel that way. Want to talk about it?",
                    "That's totally valid. What brought that on?",
                    "Thanks for sharing! How can I support you?"
                });

            return GetQuickResponse(currentMessage);
        }

        private string GetQuickJoke()
        {
            string[] jokes = new string[]
            {
                "Why do programmers prefer dark mode? Because light attracts bugs! 🐛",
                "How many programmers does it take to change a light bulb? None, that's a hardware problem! 💡",
                "Why did the developer go broke? Because he used up all his cache! 💰",
                "A SQL query walks into a bar, walks up to two tables and asks... Can I join you? 🍺",
                "Why do Java developers wear glasses? Because they don't C sharp! 👓",
                "How do you comfort a JavaScript bug? You console it! 😭",
                "Why was the Python programmer sad? Because he had a bad list comprehension! 📋"
            };
            return jokes[random.Next(jokes.Length)];
        }

        private string GetRandomFact()
        {
            string[] facts = new string[]
            {
                "Did you know? Honey never spoils. Archaeologists have found 3,000-year-old honey in Egyptian tombs that was still edible! 🍯",
                "Did you know? The Eiffel Tower grows about 6 inches taller every summer due to thermal expansion! 🗼",
                "Did you know? Octopuses have three hearts - two pump blood to the gills, one to the rest of the body! 🐙",
                "Did you know? A day on Venus is longer than a year on Venus! 🪐",
                "Did you know? Bananas are berries, but strawberries aren't! 🍌",
                "Did you know? Penguins have knees! They're just hidden under their feathers! 🐧",
                "Did you know? Sharks have been around longer than dinosaurs! 🦈"
            };
            return facts[random.Next(facts.Length)];
        }

        private string RandomResponse(string[] responses)
        {
            return responses[random.Next(responses.Length)];
        }

        public void ClearHistory()
        {
            conversationHistory.Clear();
            InitializeContextMemory();
        }

        public int GetConversationLength()
        {
            return conversationHistory.Count;
        }

        public string GetMemorySummary()
        {
            return $"Talked about: {contextMemory["last_topic"]} | Total messages: {conversationHistory.Count} | Mood: {contextMemory["mood"]}";
        }
    }
}
