import React, { useEffect, useState } from "react";
import CharacterList from "../../components/CharacterList/CharacterList";
import Pagination from "../../components/Pagination/Pagination";
import "./Locations.scss";

const Locations = () => {
    let [locationId, setLocationId] = useState(0);
    let [pageNumber, setPageNumber] = useState(1);
    let [totalPageCount, setTotalPageCount] = useState(2);
    let [locations, setLocations] = React.useState([]);
    let [characters, setCharacters] = React.useState([]);

    useEffect(() => {

        (async function () {

            let locationsResponse = await fetch("http://localhost:5016/rickandmorty/locations").then((res) => res.json());
            setLocations(locationsResponse.locations);
            setLocationId(locationsResponse.locations[0].id);
        })();

    }, []);

    useEffect(() => {

        getLocationCharacters();

    }, [locationId, pageNumber]);

    async function getLocationCharacters() {

        if (locationId == 0) return;

        var url = "http://localhost:5016/rickandmorty/characters?locationId=" + locationId + "&pageNumber=" + pageNumber + "&pageCount=3";

        let charactersResponse = await fetch(url).then((res) => res.json());
        setCharacters(charactersResponse.characters);
    }

    const locationOnClick = async (id) => {
        setLocationId(id);
    }

    return (
        <table className="locations-container">
            <tbody>
                <tr>
                    <td className="location-list">
                        {locations.map((location) => (
                            <div className="location" onClick={() => locationOnClick(location.id)} key={location.id}>
                                <div className="name">{location.name}</div>
                                <div className="locationNo">{location.dimension}</div>
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


export default Locations;
