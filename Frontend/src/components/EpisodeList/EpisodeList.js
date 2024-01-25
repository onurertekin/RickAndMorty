import React, { useRef } from "react";
import "./EpisodeList.scss";

var charactersRef = null;

const EpisodeList = ({ episodes, characters }) => {

    charactersRef = useRef(characters)

    return episodes.map((episode) => {
      
      let { episodeNo, name } = episode;

      return (
        
            <div className="episode" onClick={() => episodeOnClick({episodeNo})} key={episodeNo}>
                <div className="name">{name}</div>
                <div className="episodeNo">{episodeNo}</div>
            </div>
      );
    });
};

const episodeOnClick = (episodeNo) => {
    console.log('You clicked submit. ' + episodeNo);
    console.log(episodeNo);

    charactersRef = [
        {
          "name": "TEST 1",
          "status": 0,
          "gender": 0,
          "species": 0,
          "locationId": 0,
          "originId": 0
        },
        {
            "name": "TEST 2",
            "status": 0,
            "gender": 0,
            "species": 0,
            "locationId": 0,
            "originId": 0
          }
      ];
}

export default EpisodeList;
