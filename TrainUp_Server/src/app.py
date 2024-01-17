from flask import request, jsonify, Flask
from home import home_card_displayer
from models.user import User, db
from auth import user_login, user_register, user_logout, update_data
from flask_login import LoginManager, current_user
import os

SECRET_KEY = os.urandom(32)

app = Flask(__name__)
app.config['SECRET_KEY'] = SECRET_KEY
app.config['SQLALCHEMY_DATABASE_URI'] = 'mysql://root:12345@localhost:3309/TrainUp'
app.config['SQLALCHEMY_TRACK_MODIFICATIONS'] = False
app.config['SESSION_TYPE'] = 'filesystem'
db.init_app(app)
login_manager = LoginManager(app)
login_manager.login_view = "login"

@login_manager.user_loader
def load_user(user_id):
    return User.query.get(int(user_id))

@app.route("/login", methods=['GET', 'POST'])
def login():
    data = request.get_json()
    value = user_login(data)
    print(current_user.id)
    return value

@app.route("/register", methods=['GET', 'POST'])
def register():
    data = request.get_json()
    with app.app_context():
        return user_register(data)

@app.route("/logout", methods=['GET', 'POST'])
def logout():
    return user_logout()

@app.route("/home", methods=['GET', 'POST'])
def home():
    data = request.get_json()
    return home_card_displayer()

@app.route("/update_data", methods=['GET', 'POST'])
def update():
    data = request.get_json()
    return update_data(data)

@app.before_request
def init_db():
    with app.app_context():
        db.create_all()
        db.session.commit()


if __name__ == '__main__':
    app.run(host="0.0.0.0", port=5000)