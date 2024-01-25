import React, { useEffect, useState } from "react";
import CharacterList from "../../components/CharacterList/CharacterList";
import Pagination from "../../components/Pagination/Pagination";
import "./Episodes.scss";

const Episodes = () => {
  let [episodeId, setEpisodeId] = useState(0);
  let [pageNumber, setPageNumber] = useState(1);
  let [totalPageCount, setTotalPageCount] = useState(2);
  let [episodes, setEpisodes] = React.useState([]);
  let [characters, setCharacters] = React.useState([]);

  useEffect(() => {

    (async function () {

      let episodesResponse = await fetch("http://localhost:5016/rickandmorty/episodes").then((res) => res.json());
      setEpisodes(episodesResponse.episodes);
      //Click first episode
      //await episodeOnClick(episodesResponse.episodes[0].id);
      setEpisodeId(episodesResponse.episodes[0].id);
    })();

  }, []);

  useEffect(() => {

    getEpisodeCharacters();

  }, [episodeId, pageNumber]);

  async function getEpisodeCharacters() {

    if (episodeId == 0) return;

    var url = "http://localhost:5016/rickandmorty/episodes/" + episodeId + "/characters?pageNumber=" + pageNumber + "&pageCount=3";

    let charactersResponse = await fetch(url).then((res) => res.json());
    setCharacters(charactersResponse.episodeCharacters);
  }

  const episodeOnClick = async (id) => {
    setEpisodeId(id);
  }

  return (
    <table className="episodes-container">
      <tbody>
        <tr>
          <td className="episode-list">
            {episodes.map((episode) => (
              <div className="episode" onClick={() => episodeOnClick(episode.id)} key={episode.episodeNo}>
                <div className="name">{episode.name}</div>
                <div className="episodeNo">{episode.episodeNo}</div>
              </div>
            ))}
          </td>
          <td className="character-list">
            <div className="">
              <CharacterList characters={characters} />
            </div>
            {characters.length > 0 &&
              <Pagination totalPageCount={totalPageCount} pageNumber={pageNumber} setPageNumber={setPageNumber} />
            }

            {characters.length == 0 &&
              <div>Hiç kayıt bulunmadı</div>
            }
          </td>
        </tr>
      </tbody>
    </table>
  );
};


export default Episodes;
