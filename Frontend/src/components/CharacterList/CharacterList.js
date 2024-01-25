import React, { useRef } from "react";
import "./CharacterList.scss";

const getStatusText = (status) => {
  switch (status) {
    case 0:
      return "Unknown";
    case 1:
      return "Alive";
    case 2:
      return "Dead";
  }
};


const getGenderText = (gender) => {
  switch (gender) {
    case 0:
      return "Unknown";
    case 1:
      return "Male";
    case 2:
      return "Female";
    case 3:
      return "Genderless";
  }
};

const getSpeciesText = (species) => {
  switch (species) {
    case 0:
      return "Human";
    case 1:
      return "Alien";
    case 2:
      return "Humonoid";
    case 3:
      return "Poopybutthole";
    case 4:
      return "Mythological";
    case 5:
      return "Unknown";
    case 6:
      return "Animal";
    case 7:
      return "Disease";
    case 8:
      return "Robot";
    case 9:
      return "Cronenberg";
    case 10:
      return "Planet";
  }
};


const CharacterList = ({ characters }) => {

  return characters.map((character) => {

    let { id, name, status, gender, species } = character;
    const avatarUrl = "https://rickandmortyapi.com/api/character/avatar/" + id + ".jpeg";
    return (

      <a href={`/characters/${id}`}>
        <div className="character" key={id}>
          <div className="character-info">
            <div className="character-image">
              <img src={avatarUrl} alt={name} />
            </div>
            <div className="name">{name}</div>
            <div className="status">{getStatusText(status)}</div>
            <div className="gender">{getGenderText(gender)}</div>
            <div className="species">{getSpeciesText(species)}</div>
          </div>
        </div>
      </a>
    );
  });
};

export default CharacterList;
