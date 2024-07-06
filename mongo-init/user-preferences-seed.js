// JavaScript source code


db = db.getSiblingDB('TheNewsReporter');

db.UserPreferences.insertMany([
    {
        "_id": ObjectId(),
        "userId": ObjectId(),
        "preferences": {
            "categories": ["technology", "science"],
            "communication": {
                "channel": "email",
                "details": {
                    "email": "user1@example.com"
                }
            }
        }
    },
    {
        "_id": ObjectId(),
        "userId": ObjectId(),
        "preferences": {
            "categories": ["business", "health"],
            "communication": {
                "channel": "telegram",
                "details": {
                    "telegramId": "@user2"
                }
            }
        }
    }
]);

