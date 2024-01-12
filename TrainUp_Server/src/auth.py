from flask import Flask, render_template, flash, redirect, url_for, request, jsonify
from models.user import User, db
#from models.user import User, db
#from flask_login import login_user, current_user, LoginManager, logout_user
import os


#SECRET_KEY = os.urandom(32)
#app = Flask(__name__)
#app.config['SECRET_KEY'] = SECRET_KEY


# login_manager = LoginManager(app)
# login_manager.init_app(app)
# login_manager.login_view = "login"
#
# @login_manager.user_loader
# def load_user(user_id):
#     if user_id is not None:
#         return User.query.get(int(user_id))
#     return None
#
# @app.route("/register", methods=['GET', 'POST'])
# def register():
#     pass
    # if current_user.is_authenticated:
    #     id_user = current_user.id
    #     url_redirezione = f'http://localhost:5009/cityevents/{id_user}'
    #     return redirect(url_redirezione)
    # form = RegistrationForm()
    # if form.validate_on_submit():
    #     user = User(username=form.data.get('username'), password=form.data.get('password'))
    #     db.session.add(user)  #Questa riga aggiunge il nuovo oggetto utente creato alla sessione SQLAlchemy.
    #     login_user(user)        #consente all'applicazione di tenere traccia dell'utente autenticato.
    #     db.session.commit()  #Questa riga conferma le modifiche apportate alla sessione del database, aggiungendo effettivamente il nuovo utente al database.
    #     flash(f'Account created for {form.username.data}!', 'success')
    #     id_user = current_user.id
    #     url_redirezione = f'http://localhost:5009/cityevents/{id_user}'
    #     return redirect(url_redirezione)
    # return render_template('register.html', title='Register', form=form)

def user_login(data=None):
    print(db.session.query(User).first())
    if db.session.query(User).filter(User.username == data.get('username'), User.password == data.get('password')).first():
        return jsonify({'state': '1'})
    else:
        return jsonify({'state': 'wrong credentials'})


# if current_user.is_authenticated:
    #     id_user = current_user.id
    #     url_redirezione = f'http://localhost:5009/cityevents/{id_user}'
    #     return redirect(url_redirezione)
    #form = LoginForm()
    #if form.validate_on_submit():
    #if db.session.query(User).filter(User.username == form.data.get('username'), User.password == form.data.get('password')).first():
    #         user = db.session.query(User).filter(User.username == form.data.get('username'), User.password == form.data.get('password')).first()
    #         login_user(user)
    #         flash('You have been logged in!', 'success')
    #         id_user = current_user.id
    #         url_redirezione = f'http://localhost:5009/cityevents/{id_user}'
    #         return redirect(url_redirezione)
    #
    #     else:
    #         flash('Login Unsuccessful. Please check username and password', 'danger')
    # return render_template('login.html', title='Login', form=form)

# @app.route("/log_out", methods=['GET', 'POST'])
# def log_out():
#     logout_user()
#     return redirect(url_for('login'))
#
#

