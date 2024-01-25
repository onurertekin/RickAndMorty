import React from "react";
import "./TopMenu.scss";

const TopMenu = () => {
  return (
    <div className="top-menu">
        <a href="/" className="menu-item">Home</a>
        <a href="/episodes" className="menu-item">Episodes</a>
        <a href="/locations" className="menu-item">Locations</a>
    </div>
  );
};

export default TopMenu;
