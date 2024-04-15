import logging
from flask import jsonify
from models.user import User, db
from urllib.parse import quote
import jwt

SECRET_KEY = "mysecretkey"

invalid_tokens = []  # Lista per memorizzare i token invalidati

def user_login(data=None):
    #controllo campi vuoti
    if data.get("username") == '' or data.get("password") == '':
        return jsonify({'state': 'fault'})

    user = db.session.query(User).filter(User.username == data.get('username'), User.password == data.get('password')).first()
    if user:
        #controllo lista token invalidati
        if user.id in invalid_tokens:
            invalid_tokens.remove(user.id)

        # Genera un nuovo token valido
        token = jwt.encode({'user_id': user.id}, key=SECRET_KEY, algorithm='HS256')
        logging.error(user.id)
        logging.error(invalid_tokens)
        return jsonify({'state': 'success', 'token': token})
    else:
        return jsonify({'state': 'fault'})

def user_register(data=None):

    if data.get("username") == '' or data.get("password") == '' or data.get("confirmPassword") == '' or data.get("age") == '' or data.get("weight") == '':
        return jsonify({'state': 'fault1'})
    elif data.get('password') == data.get('confirmPassword'):
        user = User(username=data.get('username'), password=data.get('password'), age= data.get('age'), weight= data.get('weight'))
        db.session.add(user)
        db.session.commit()

        #controllo lista token invalidati
        if user.id in invalid_tokens:
            invalid_tokens.remove(user.id)

        # Genera un nuovo token valido
        token = jwt.encode({'user_id': user.id}, key=SECRET_KEY, algorithm='HS256')
        logging.error(user.id)
        logging.error(invalid_tokens)
        return jsonify({'state': 'success', 'token': token})
    else:
        return jsonify({'state': 'fault2'})


def user_logout(data=None):
    token = data.get('token')
    encoded_token = quote(token)
    decoded_token = jwt.decode(encoded_token, key=SECRET_KEY, algorithms=['HS256'])
    user_id = decoded_token['user_id']
    logging.error(decoded_token['user_id'])

    if user_id is not None:
        invalid_tokens.append(user_id)
        logging.error(invalid_tokens)
        return jsonify({'state': 0})
    else:
        return jsonify({'state': 1})


def update_data(data=None):
    token = data.get('token')
    encoded_token = quote(token)
    decoded_token = jwt.decode(encoded_token, key=SECRET_KEY, algorithms=['HS256'])
    logging.error(decoded_token['user_id'])

    if data.get("data") == '':
        return jsonify({'state': 0})
    elif db.session.query(User).filter(User.age == data.get('data1')):
        new_age = int(data.get('data'))
        db.session.query(User).filter(User.id == decoded_token['user_id']).update({'age': new_age})
        db.session.commit()
        return jsonify({'state': 1})
    elif db.session.query(User).filter(User.weight == data.get('dat1')):
        new_weight = int(data.get('data'))
        db.session.query(User).filter(User.id == decoded_token['user_id']).update({'weight': new_weight})
        db.session.commit()
        return jsonify({'state': 1})


def update_password(data=None):
    token = data.get('token')
    encoded_token = quote(token)
    decoded_token = jwt.decode(encoded_token, key=SECRET_KEY, algorithms=['HS256'])
    user_id = decoded_token['user_id']
    logging.error(decoded_token['user_id'])

    if data.get('nuovaPassword') == '' or data.get('vecchiaPassword') == '' or data.get('confermaPassword') == '':
        return jsonify({'state': 1})
    elif data.get('nuovaPassword') == data.get('confermaPassword'):
        if user_id is not None:
            user = db.session.query(User).filter(User.id == user_id,User.password == data.get('vecchiaPassword')).first()
            if user:
                user.password = data.get('nuovaPassword')
                db.session.commit()
                return jsonify({'state': 0})
            else: return jsonify({'state': 1})
    else: return jsonify({'state': 1})