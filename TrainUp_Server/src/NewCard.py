from urllib.parse import quote
from flask import jsonify
from sqlalchemy import func
import logging
import jwt
from home import fitness_app_singleton
from models.user import TrainingCard_, db, ExercisesCards_, User

SECRET_KEY = "mysecretkey"

#FUNZIONE PER LA CREAZIONE DI UNA NUOVA TRAINING CARD
def New_card(data=None):
    token = data.get('token')
    encoded_token = quote(token)
    decoded_token = jwt.decode(encoded_token, key=SECRET_KEY, algorithms=['HS256'])
    user_id = decoded_token['user_id']

    #ottengo l'ultimo id dal db
    last_id = db.session.query(func.max(TrainingCard_.id)).scalar()
    logging.error(f"id ultima scheda {last_id}")
    # Se non ci sono schede nel database, l'ultimo ID sarà None
    # In tal caso, impostiamo last_id a 0 per iniziare da 1
    if last_id is None:
        last_id = 0

    newCard_id = last_id +1

    Create_new_card = fitness_app_singleton.create_training_card(user_id=user_id,card_id=newCard_id)
    logging.error(f"id nuova scheda: {Create_new_card.card_id}")
    logging.error(Create_new_card.user_id)
    updated_user_cards = fitness_app_singleton.get_user_training_cards(user_id=user_id)

    return jsonify({'user_cards': updated_user_cards})

def add_exercise(data=None):
    token = data.get('token')
    encoded_token = quote(token)
    decoded_token = jwt.decode(encoded_token, key=SECRET_KEY, algorithms=['HS256'])
    id = decoded_token['user_id']


    train_card = data.get("train_card")
    cardIid = train_card.get('card_id')
    exercises = train_card.get('exercises')

    for exercise in exercises:
        if exercise['day'] == '' or exercise['name'] == '' or exercise['reps'] == 0 or exercise['sets']== 0:
            return jsonify({'state': 0})
        day = exercise['day']
        name = exercise['name']
        reps = exercise['reps']
        sets = exercise['sets']

    fitness_app_singleton.add_exercise_to_card(cardIid, exercise_name=name, sets=sets, reps=reps, day=day)
    logging.error(fitness_app_singleton.get_user_training_cards(id))

    return jsonify({'state': 1})


def confirm_creation_card(data=None):
    token = data.get('token')
    encoded_token = quote(token)
    decoded_token = jwt.decode(encoded_token, key=SECRET_KEY, algorithms=['HS256'])
    id = decoded_token['user_id']

    user_cards = fitness_app_singleton.get_user_training_cards(user_id=id)
    card = user_cards[-1]

    if data.get('name') == '':
        return jsonify({'state': 0})
    name_card = data.get('name')
    _card_ = TrainingCard_(id=card["card_id"],user_id=id,name=name_card)
    db.session.add(_card_)

    exercises = card["exercises"]
    for exe in exercises:
        _exe_ = ExercisesCards_(id_train_card=card["card_id"],name=exe["name"],sets=exe["sets"],reps=exe["reps"],day=exe["day"])
        db.session.add(_exe_)


    db.session.commit()

    return jsonify({'state': 1})


#nel caso in cui un utente apre la pagina di creazione di una scheda ma prima di inserirla nel db
#torna alla pagina precedente allora la scheda va eliminata altrimenti aprendo nuovamnete
#la pagina di creazione ne verrà creata una nuova
def remove_from_list(data=None):
    token = data.get('token')
    encoded_token = quote(token)
    decoded_token = jwt.decode(encoded_token, key=SECRET_KEY, algorithms=['HS256'])
    id = decoded_token['user_id']

    user_cards = fitness_app_singleton.get_user_training_cards(user_id=id)
    user_cards.pop(-1)
    logging.error("dopo remove")
    logging.error(fitness_app_singleton.get_user_training_cards(id))
    return jsonify({'state': 1})


def delete_trainingCard(data=None):
    token = data.get('token')
    encoded_token = quote(token)
    decoded_token = jwt.decode(encoded_token, key=SECRET_KEY, algorithms=['HS256'])
    id = decoded_token['user_id']

    if id is not None:
        user= db.session.query(User).filter(User.id == id, User.password== data.get('password_')).first()
        if user:
            #Recupero la scheda da eliminare
            scheda_da_elimi = db.session.query(TrainingCard_).filter(TrainingCard_.id == data.get('id_scheda') ).first()
            if scheda_da_elimi:
                #recupero tutti gli esercizi di quella scheda
                esercizi_da_elim = db.session.query(ExercisesCards_).filter(ExercisesCards_.id_train_card == data.get('id_scheda')).all()
                for ese in esercizi_da_elim:
                    db.session.delete(ese)

                db.session.delete(scheda_da_elimi)
                db.session.commit()
                return jsonify({'state': 0})
            else: return jsonify({'state': 1})
        else:return jsonify({'state': 1})