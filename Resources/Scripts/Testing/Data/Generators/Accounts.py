import csv
import random
import string
import re
import base64

num_records = 50000

pan_regex = r"^[A-Z]{5}[0-9]{4}[A-Z]{1}$"
gst_regex = r"^\d{2}[A-Z]{5}\d{4}[A-Z]{1}[A-Z\d]{1}[Z]{1}[A-Z\d]{1}$"

currencies = ['INR', 'USD', 'EUR', 'GBP', 'JPY', 'AUD', 'CAD', 'CHF', 'CNY', 'SEK']

def generate_pan():
  return ''.join(random.choices(string.ascii_uppercase, k=5)) + ''.join(random.choices(string.digits, k=4)) + random.choice(string.ascii_uppercase)

def generate_gst():
  return random.choice(string.digits * 2) + ''.join(random.choices(string.ascii_uppercase, k=5)) + ''.join(random.choices(string.digits, k=4)) + random.choice(string.ascii_uppercase) + random.choice(string.ascii_uppercase + string.digits) + 'Z' + random.choice(string.ascii_uppercase + string.digits)

def generate_description():
  words = ['Test', 'Account', 'Customer', 'Company', 'Business', 'Personal', 'Corporate']
  return '-'.join(random.sample(words, 2))

def generate_name(i):
  words = ['Test', 'Account', 'Customer', 'Company', 'Business', 'Personal', 'Corporate'] 
  return '-'.join(random.sample(words, 2)) + ' ' + str(i)
  
def generate_balance():
  return round(random.uniform(0, 1000), 2)

with open('accounts.csv', 'w', newline='') as f:
  writer = csv.writer(f)
  writer.writerow(['GSTNumber', 'PANNumber', 'Balance', 'Currency', 'Description', 'Name'])

  for i in range(num_records):
    pan = generate_pan()
    gst = generate_gst()
    balance = generate_balance()
    currency = random.choice(currencies)
    description = generate_description()
    name = generate_name(i)

    writer.writerow([gst, pan, balance, currency, description, name])

with open("accounts.csv", "rb") as f:
  data = f.read()

base64_string = base64.b64encode(data).decode('utf-8')

with open("accounts_base64.txt", "w") as f:
  f.write(base64_string)
  
print("Base64 encoded string written to accounts_base64.txt")
print("CSV file with {} records generated.".format(num_records))
