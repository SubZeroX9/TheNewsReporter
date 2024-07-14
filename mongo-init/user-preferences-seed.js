// JavaScript source code

db = db.getSiblingDB('TheNewsReporter');

db.UserPreferences.insertMany([
    {
        "_id": ObjectId(),
        "user_id": ObjectId(),
        "prefered_categories": ["technology", "science"],
        "news_preferences": ["space" , "breakthroughs", "inventions"],
        "communication_channel": {
            "channel": "Email",
            "details": {
                "email": "rafael.azrv@gmail.com"
            }
        }
    },
    {
        "_id": ObjectId(),
        "user_id": ObjectId(),
        "prefered_categories": ["business", "health"],
        "news_preferences": ["stocks", "breakthroughs","sells","diet"],
        "communication_channel": {
            "channel": "Telegram",
            "details": {
                "telegramId": "@user2"
            }
        }
    }
]);

