import TrainingCard
from flask import Flask, render_template, flash, redirect, url_for, request, jsonify
from auth import user_login
from models.user import User, db
#from flask_login import login_user, current_user, LoginManager, logout_user
import os

class FitnessApp:
    _instance = None

    def __new__(cls):
        if not cls._instance:
            cls._instance = super(FitnessApp, cls).__new__(cls)
            cls._instance.training_cards = []

        return cls._instance

    def create_training_card(self, user_id):
        card_id = len(self.training_cards) + 1
        new_card = TrainingCard(card_id, user_id)
        self.training_cards.append(new_card)
        return new_card

    def add_exercise_to_card(self, card_id, exercise_name, sets, reps, day):
        card = next((card for card in self.training_cards if card.card_id == card_id), None)
        if card:
            card.add_exercise(exercise_name, sets, reps, day)
            return True
        else:
            return False

    def get_user_training_cards(self, user_id):
        user_cards = [card.get_card_details() for card in self.training_cards if card.user_id == user_id]
        return user_cards

# # Esempio di utilizzo
# fitness_app_singleton = FitnessApp()
#
# # Creare una nuova scheda di allenamento per un utente
# new_card = fitness_app_singleton.create_training_card(user_id=1)
#
# # Aggiungere esercizi alla scheda di allenamento
# fitness_app_singleton.add_exercise_to_card(new_card.card_id, "Push-ups", sets=3, reps=15, day="Monday")
# fitness_app_singleton.add_exercise_to_card(new_card.card_id, "Squats", sets=4, reps=12, day="Wednesday")
#
# # Ottenere tutte le schede di allenamento per un utente
# user_cards = fitness_app_singleton.get_user_training_cards(user_id=1)
# print("Schede di allenamento dell'utente:")
# print(user_cards)

SECRET_KEY = os.urandom(32)

app = Flask(__name__)
app.config['SECRET_KEY'] = SECRET_KEY


@app.route("/login", methods=['GET', 'POST'])
def login():
    data = request.get_json()
    return user_login(data)

@app.route("/home", methods=['GET', 'POST'])
def home():
    data = request.get_json()
    return user_login(data)

@app.before_request
def init_db():
    with app.app_context():
        db.create_all()
        db.session.commit()

if __name__ == '__main__':
    app.run(host="0.0.0.0", port=5000)