from io import BytesIO
from flask import send_file
from sqlalchemy import func
from models.user import TrainingCard_, db, cardsFavoreites
import matplotlib.pyplot as plt

SECRET_KEY = "mysecretkey"

def cards_most_used():

    # Esegui una query per ottenere il numero di volte che ogni scheda è stata salvata tra i preferiti dagli utenti
    card_usage = db.session.query(cardsFavoreites.id_card, func.count(cardsFavoreites.id_card)).group_by(cardsFavoreites.id_card).all()

    # Estrai i dati dalla query
    card_ids, usage_counts = zip(*card_usage)
    usage_counts = [int(round(count)) for count in usage_counts]

    # Crea il grafico
    plt.bar(card_ids, usage_counts)
    plt.xlabel('ID Scheda di Allenamento')
    plt.ylabel('Numero di Utenti che l\'hanno Salvata')
    plt.title('Schede di Allenamento Più Usate dagli Utenti')
    plt.yticks(range(0, 21, 2))

    # Salva il grafico nell'oggetto BytesIO
    buffer = BytesIO()
    plt.savefig(buffer, format='png')
    buffer.seek(0)

    # Invia il file immagine come risposta
    return send_file(buffer, mimetype='image/png')
