from flask import jsonify
from models.user import User, db

def user_login(data=None):
    if db.session.query(User).filter(User.username == data.get('username'), User.password == data.get('password')).first():
        return jsonify({'state': '1'})
    else:
        return jsonify({'state': 'wrong credentials'})

def user_register(data=None):
    if data.get('password') == data.get('confirmPassword'):
        user = User(username=data.get('username'), password=data.get('password'), age= data.get('age'), weight= data.get('weight'))
        db.session.add(user)
        db.session.commit()
        return jsonify({'state': '1'})
    else:
        return jsonify({'state': 'wrong credentials'})

