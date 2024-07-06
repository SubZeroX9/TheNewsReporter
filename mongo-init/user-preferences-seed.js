// JavaScript source code

db = db.getSiblingDB('TheNewsReporter');

db.UserPreferences.insertMany([
    {
        "_id": ObjectId(),
        "user_id": ObjectId(),
        "prefered_categories": ["technology", "science"],
        "communication_channel": {
            "channel": "Email",
            "details": {
                "email": "user1@example.com"
            }
        }
    },
    {
        "_id": ObjectId(),
        "user_id": ObjectId(),
        "prefered_categories": ["business", "health"],
        "communication_channel": {
            "channel": "Telegram",
            "details": {
                "telegramId": "@user2"
            }
        }
    }
]);

