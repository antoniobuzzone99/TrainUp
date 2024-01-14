from FitnessApp import FitnessApp
from TrainingCard import TrainingCard
from flask import Flask, render_template, flash, redirect, url_for, request, jsonify
from auth import user_login, user_register
from home import home_card_displayer
from models.user import User, db
#from flask_login import login_user, current_user, LoginManager, logout_user
import os

SECRET_KEY = os.urandom(32)

app = Flask(__name__)
app.config['SECRET_KEY'] = SECRET_KEY
app.config['SQLALCHEMY_DATABASE_URI'] = 'mysql://root:0000@localhost:3306/TrainUp'
app.config['SQLALCHEMY_TRACK_MODIFICATIONS'] = False
db.init_app(app)


@app.route("/login", methods=['GET', 'POST'])
def login():
    data = request.get_json()
    return user_login(data)

@app.route("/register", methods=['GET', 'POST'])
def register():
    data = request.get_json()
    with app.app_context():
        return user_register(data)

@app.route("/home", methods=['GET', 'POST'])
def home():
    data = request.get_json()
    user_cards = fitness_app_singleton.get_user_training_cards(user_id=1)

    return home_card_displayer(user_cards)


@app.before_request
def init_db():
    with app.app_context():
        db.create_all()
        db.session.commit()


if __name__ == '__main__':

    fitness_app_singleton = FitnessApp()

    # Creare una nuova scheda di allenamento per un utente
    new_card = fitness_app_singleton.create_training_card(user_id=1)

    # Aggiungere esercizi alla scheda di allenamento
    fitness_app_singleton.add_exercise_to_card(new_card.card_id, "Push-ups", sets=3, reps=15, day="Monday")
    fitness_app_singleton.add_exercise_to_card(new_card.card_id, "Squats", sets=4, reps=12, day="Wednesday")

    user_cards = fitness_app_singleton.get_user_training_cards(user_id=1)
    with app.app_context():
        print(home_card_displayer(user_cards))
    #print(user_cards)

    app.run(host="0.0.0.0", port=5000)