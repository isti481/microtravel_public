## ⚠️ Fejlesztés alatt

Oldal elérhetősége: https://fejlesztooldal.runasp.net/

## ℹ️ Az alkalmazás célja

A **MicroTravel** webalkalmazás célja, hogy a felhasználók egyszerűen böngészhessenek az elérhető utazások között, és online foglalást adhassanak le.

## Oldal funkciók

- regisztráció
- belépés
- utazások megtekintése
- utazás foglalása

Admin felhasználó: admin@admin.hu
jelszó: Egy23456*

Admin funkciók:
- Utazás létrehozása
- Utazás megtekintése
- Utazás módosítása
- Utazás törlése
- Utazás engedélyezése (publikálása az oldalon)

## REST API
Content-Type application/json

autentikáció: 
key:token
value:dzAAeYvO*HUbppD7jh9Sc
GET (http://fejlesztooldal.runasp.net/api/travels)
GET (http://fejlesztooldal.runasp.net/api/travels/1)

POST, DELETE, PUT kérésekre
autentikáció: 
key:token
value:7C561*gC9F50

POST http://fejlesztooldal.runasp.net/api/travels
teszt adat:
  {
    "name": "Utazás Budapestre",
    "travelTypeId": 4,
    "travelDealTypeId": 1,
    "description": "Nullam ultrices laoreet purus, non tempor ante egestas eu. Etiam mollis pretium consectetur. Aliquam erat volutpat. Etiam quis ipsum et turpis maximus viverra.",
    "price": 80,
    "travelPictureUrl": "/images/utazas_budapestre.jpg",
    "travelDate": "2026-03-05T14:11:00"
  }
  
PUT http://fejlesztooldal.runasp.net/api/travels/5
teszt adat:
  {
    "id": 5,
      "name": "Utazás Budapestre3",
      "travelTypeId": 6,
      "travelDealTypeId": 2,
      "description": "Nullam ultrices laoreet purus, non tempor ante egestas eu. Etiam mollis pretium consectetur. Aliquam erat volutpat. Etiam quis ipsum et turpis maximus viverra.",
      "price": 81,
      "travelPictureUrl": "/images/utazas_budapestre.jpg",
      "travelDate": "2026-03-05T14:11:00",
      "enabled": 0
  }
  
DELETE http://fejlesztooldal.runasp.net/api/travels/5
  {
    "id": 5
  }


