import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

import "./Character.scss";

const Character = () => {
  let [character, setCharacter] = React.useState([]);
  let { id } = useParams();

  useEffect(() => {

    (async function () {

      let characterResponse = await fetch("http://localhost:5016/rickandmorty/characters/" + id).then((res) => res.json());
      characterResponse.id = id;
      setCharacter(characterResponse);
    })();

  }, []);

 
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

  
  return (
    <div className="character-container">

      <img src={"https://rickandmortyapi.com/api/character/avatar/" + character.id + ".jpeg"} alt={character.name} />
      <div className="character-info">
        <div className="name">{character.name}</div>
        <div className="status">{getStatusText(character.status)}</div>
            <div className="gender">{getGenderText(character.gender)}</div>
            <div className="species">{getSpeciesText(character.species)}</div>
      </div>
    </div>
  );
};


export default Character;
