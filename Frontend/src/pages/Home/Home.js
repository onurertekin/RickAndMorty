import React, { useEffect, useState } from "react";
import CharacterList from "../../components/CharacterList/CharacterList";
import Pagination from "../../components/Pagination/Pagination";
import "./Home.scss";

const Episodes = () => {
    let [pageNumber, setPageNumber] = useState(1);
    let [totalPageCount, setTotalPageCount] = useState(2);
    let [characters, setCharacters] = React.useState([]);

    let [name, setName] = React.useState("");
    let [status, setStatus] = React.useState(-1);
    let [gender, setGenders] = React.useState(-1);
    let [species, setSpecies] = React.useState(-1);

    useEffect(() => {

        searchCharacters();

    }, [pageNumber]);

    async function searchCharacters() {

        var url = "http://localhost:5016/rickandmorty/characters?pageNumber=" + pageNumber + "&pageCount=3";

        if (name != "")
            url += "&name=" + name;
        if (status != -1)
            url += "&status=" + status;
        if (gender != -1)
            url += "&gender=" + gender;
        if (species != -1)
            url += "&species=" + species;

        let charactersResponse = await fetch(url).then((res) => res.json());
        setCharacters(charactersResponse.characters);
    }

    const nameOnChange = async (event) => {
        console.log(event.target.value);
        setName(event.target.value);
    };

    const statusOnChange = async (event) => {
        setGenders(event.target.value);
    };

    const genderOnChange = async (event) => {
        setStatus(event.target.value);
    };

    const speciesOnChange = async (event) => {
        setSpecies(event.target.value);
    };

    const searchOnClick = async () => {
        searchCharacters();
    };

    return (
        <table className="home-container">
            <tbody>
                <tr>
                    <td className="filters">

                        <div className="filter">
                            <div className="filter-label">Name</div>
                            <input onChange={nameOnChange} onInput={nameOnChange} />
                        </div>

                        <div className="filter">
                            <div className="filter-label">Status</div>
                            <select onChange={statusOnChange}>
                                <option value="-1">All</option>
                                <option value="1">Alive</option>
                                <option value="2">Dead</option>
                                <option value="0">Unknown</option>
                            </select>
                        </div>

                        <div className="filter">
                            <div className="filter-label">Gender</div>
                            <select onChange={genderOnChange}>
                                <option value="-1">All</option>
                                <option value="0">Unknown</option>
                                <option value="1">Male</option>
                                <option value="2">Female</option>
                                <option value="3">Genderless</option>
                            </select>
                        </div>

                        <div className="filter">
                            <div className="filter-label">Species</div>
                            <select onChange={speciesOnChange}>
                                <option value="-1">All</option>
                                <option value="0">Human</option>
                                <option value="1">Alien</option>
                                <option value="2">Humanoid</option>
                                <option value="3">Poopybutthole</option>
                                <option value="4">Mythological</option>
                                <option value="5">Unknown</option>
                                <option value="6">Animal</option>
                                <option value="7">Disease</option>
                                <option value="8">Robot</option>
                                <option value="9">Cronenberg</option>
                                <option value="10">Planet</option>
                            </select>
                        </div>

                        <div className="search-button-wrapper">
                            <input type="button" value="Search" onClick={searchOnClick}></input>
                        </div>

                    </td>
                    <td className="character-list">
                        <div className="">
                            <CharacterList characters={characters} />
                        </div>
                        <Pagination totalPageCount={totalPageCount} pageNumber={pageNumber} setPageNumber={setPageNumber} />
                    </td>
                </tr>
            </tbody>
        </table>
    );
};


export default Episodes;
