from flask import jsonify


def home_card_displayer(user_cards=None):
    return jsonify(user_cards)
