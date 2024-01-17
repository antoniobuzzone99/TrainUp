from flask import jsonify
from FitnessApp import FitnessApp

def home_card_displayer():
    fitness_app_singleton = FitnessApp()
    user_cards = fitness_app_singleton.get_user_training_cards(user_id=1)

    # Controllare se ci sono già elementi con id 1 e 2 nelle schede esistenti
    ids_to_check = {1, 2, 3}
    if not any(card['card_id'] in ids_to_check for card in user_cards):
        # Creare una nuova scheda di allenamento per un utente
        new_card = fitness_app_singleton.create_training_card(user_id=1)
        # Aggiungere esercizi alla scheda di allenamento
        fitness_app_singleton.add_exercise_to_card(new_card.card_id, "Squat", sets=3, reps=5, day="Monday")
        fitness_app_singleton.add_exercise_to_card(new_card.card_id, "Step-up", sets=4, reps=5, day="Monday")
        fitness_app_singleton.add_exercise_to_card(new_card.card_id, "Leg Extension", sets=3, reps=10, day="Monday")
        fitness_app_singleton.add_exercise_to_card(new_card.card_id, "Leg Curl", sets=3, reps=15, day="Monday")
        fitness_app_singleton.add_exercise_to_card(new_card.card_id, "Calf machine", sets=4, reps=20, day="Monday")
        fitness_app_singleton.add_exercise_to_card(new_card.card_id, "Panca piana", sets=5, reps=5, day="Wednesday")
        fitness_app_singleton.add_exercise_to_card(new_card.card_id, "Military-Press", sets=6, reps=6, day="Wednesday")
        fitness_app_singleton.add_exercise_to_card(new_card.card_id, "spinte panca inclinata", sets=4, reps=8, day="Wednesday")
        fitness_app_singleton.add_exercise_to_card(new_card.card_id, "Push-down", sets=4, reps=8, day="Wednesday")
        fitness_app_singleton.add_exercise_to_card(new_card.card_id, "Alzate laterali", sets=4, reps=12, day="Wednesday")
        fitness_app_singleton.add_exercise_to_card(new_card.card_id, "Stacco da terra", sets=5, reps=3, day="Friday")
        fitness_app_singleton.add_exercise_to_card(new_card.card_id, "Trazioni", sets=5, reps=3, day="Friday")
        fitness_app_singleton.add_exercise_to_card(new_card.card_id, "Lat-machine", sets=4, reps=5, day="Friday")
        fitness_app_singleton.add_exercise_to_card(new_card.card_id, "Pull down", sets=3, reps=12, day="Friday")
        fitness_app_singleton.add_exercise_to_card(new_card.card_id, "Curl con bilancere", sets=4, reps=8, day="Friday")

        new_card1 = fitness_app_singleton.create_training_card(user_id=1)
        fitness_app_singleton.add_exercise_to_card(new_card1.card_id, "Panca piana", sets=4, reps=5, day="Monday")
        fitness_app_singleton.add_exercise_to_card(new_card1.card_id, "Lento dietro", sets=4, reps=6, day="Monday")
        fitness_app_singleton.add_exercise_to_card(new_card1.card_id, "Lat Machine", sets=4, reps=5, day="Monday")
        fitness_app_singleton.add_exercise_to_card(new_card1.card_id, "Pulley basso", sets=4, reps=6, day="Monday")
        fitness_app_singleton.add_exercise_to_card(new_card1.card_id, "Spinte avanti con corda", sets=4, reps=8, day="Monday")
        fitness_app_singleton.add_exercise_to_card(new_card1.card_id, "Stacco da terra", sets=5, reps=3, day="Wednesday")
        fitness_app_singleton.add_exercise_to_card(new_card1.card_id, "Trazioni", sets=5, reps=3, day="Wednesday")
        fitness_app_singleton.add_exercise_to_card(new_card1.card_id, "Rematore con bilancere", sets=4, reps=6, day="Wednesday")
        fitness_app_singleton.add_exercise_to_card(new_card1.card_id, "Pull-down", sets=3, reps=12, day="Wednesday")
        fitness_app_singleton.add_exercise_to_card(new_card1.card_id, "Posteriori a busto flesso", sets=3, reps=12, day="Wednesday")
        fitness_app_singleton.add_exercise_to_card(new_card1.card_id, "Squat", sets=5, reps=3, day="Friday")
        fitness_app_singleton.add_exercise_to_card(new_card1.card_id, "Step up", sets=4, reps=5, day="Friday")
        fitness_app_singleton.add_exercise_to_card(new_card1.card_id, "Stacchi a gambe tese", sets=4, reps=6, day="Friday")
        fitness_app_singleton.add_exercise_to_card(new_card1.card_id, "Leg extension", sets=3, reps=10, day="Friday")
        fitness_app_singleton.add_exercise_to_card(new_card1.card_id, "Leg Curl", sets=3, reps=10, day="Friday")


        new_card2 = fitness_app_singleton.create_training_card(user_id=1)
        fitness_app_singleton.add_exercise_to_card(new_card2.card_id, "Leg press inclinata", sets=4, reps=8, day="Monday")
        fitness_app_singleton.add_exercise_to_card(new_card2.card_id, "Chest press", sets=4, reps=8, day="Monday")
        fitness_app_singleton.add_exercise_to_card(new_card2.card_id, "Squat", sets=4, reps=10, day="Monday")
        fitness_app_singleton.add_exercise_to_card(new_card2.card_id, "Leg Curl", sets=4, reps=8, day="Monday")
        fitness_app_singleton.add_exercise_to_card(new_card2.card_id, "Alzate laterali", sets=3, reps=15, day="Monday")
        fitness_app_singleton.add_exercise_to_card(new_card2.card_id, "Stacco da terra", sets=5, reps=3, day="Wednesday")
        fitness_app_singleton.add_exercise_to_card(new_card2.card_id, "Trazioni", sets=5, reps=6, day="Wednesday")
        fitness_app_singleton.add_exercise_to_card(new_card2.card_id, "Lat Machine", sets=4, reps=8, day="Wednesday")
        fitness_app_singleton.add_exercise_to_card(new_card2.card_id, "Curl con bilancere", sets=5, reps=5, day="Wednesday")
        fitness_app_singleton.add_exercise_to_card(new_card2.card_id, "Curl ai cavi alti", sets=2, reps=12, day="Wednesday")
        fitness_app_singleton.add_exercise_to_card(new_card2.card_id, "Panca piana", sets=4, reps=6, day="Friday")
        fitness_app_singleton.add_exercise_to_card(new_card2.card_id, "Spinte panca inclinata", sets=4, reps=8, day="Friday")
        fitness_app_singleton.add_exercise_to_card(new_card2.card_id, "Push-down", sets=3, reps=15, day="Friday")
        fitness_app_singleton.add_exercise_to_card(new_card2.card_id, "Dips", sets=3, reps=15, day="Friday")
        fitness_app_singleton.add_exercise_to_card(new_card2.card_id, "Alzate laterali", sets=4, reps=12, day="Friday")

    updated_user_cards = fitness_app_singleton.get_user_training_cards(user_id=1)

    return jsonify({'user_cards': updated_user_cards})

