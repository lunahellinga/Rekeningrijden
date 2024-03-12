"""
This program reads two csv files and merges them based on a common key column.
"""
# import the pandas library
# you can install using the following command: pip install pandas

import pandas as pd

# Read the files into two dataframes.
df1 = pd.read_csv('Open_Data_RDW__Gekentekende_voertuigen.csv',
                  dtype={"Kenteken": "string[pyarrow]", "Europese voertuigcategorie": "category"})
df2 = pd.read_csv('Open_Data_RDW__Gekentekende_voertuigen_brandstof.csv',
                  dtype={"Kenteken": "string[pyarrow]", "Brandstof omschrijving": "category"})

# Merge the two dataframes, using _ID column as key
df3 = pd.merge(df1, df2, on='Kenteken')
df3 = df3.rename(
    {"Kenteken": "licence",
     "Europese voertuigcategorie": "classification",
     "Brandstof omschrijving": "fuelType"},
    axis=1)
df3 = df3.replace({
    "Benzine":"Petrol",
    "Diesel":"Diesel",
    "Elektriciteit":"Electricity",
    "Waterstof":"Hydrogen",
})
df3 = df3.dropna(how="any", ignore_index=True)
df3 = df3.sample(2_000_000)

df3 = df3.set_index('licence')

# Write it to a new CSV file
df3.to_csv('vehicles.csv')
