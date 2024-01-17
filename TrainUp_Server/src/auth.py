from flask import jsonify
from models.user import User, db
from flask_login import login_user, logout_user, current_user

def user_login(data=None):
    if db.session.query(User).filter(User.username == data.get('username'), User.password == data.get('password')).first():
        user = db.session.query(User).filter(User.username == data.get('username'), User.password == data.get('password')).first()
        login_user(user)
        id_user = current_user.id
        print(id_user)
        return jsonify({'state': 1, 'id_user': id_user})
    else:
        return jsonify({'state': 0})

def user_register(data=None):

    if data.get("username") == '' or data.get("password") == '' or data.get("confirmPassword") == '' or data.get("age") == '' or data.get("weight") == '':
        return jsonify({'state': 1})
    elif data.get('password') == data.get('confirmPassword'):
        user = User(username=data.get('username'), password=data.get('password'), age= data.get('age'), weight= data.get('weight'))
        db.session.add(user)
        db.session.commit()
        login_user(user)
        id_user = current_user.id
        print(id_user)
        return jsonify({'state': 0})
    else:
        return jsonify({'state': 2})


def user_logout():
    logout_user()
    return jsonify({'state': 0})


def update_data(data=None):
    if not current_user.is_authenticated:
        print("user non autenticato")
    print(current_user.id)
    if db.session.query(User).filter(User.age == data.get('data1')):
        new_age = int(data.get('data'))
        db.session.query(User).filter(User.id == current_user.id).update({'age': new_age})
        db.session.commit()
        return jsonify({'state': 1})
    elif db.session.query(User).filter(User.weight == data.get('dat1')):
        new_weight = int(data.get('data'))
        db.session.query(User).filter(User.id == current_user.id).update({'weight': new_weight})
        db.session.commit()
        return jsonify({'state': 1})

    return jsonify({'state': 0})