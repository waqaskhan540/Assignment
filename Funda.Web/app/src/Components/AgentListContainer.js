import React, { Component } from "react";
import "./agent-list.css";
const AgentListContainer = ({ heading, items, loading, error, onClick }) => (
    <div>
        <h3>{heading}</h3>
        
        {loading && (<p>loading...</p>)}
        {!loading && error && (<p className="error">{error}</p>)}
        {!loading && !error && items.length &&
            (<table>
                <tbody>
                    <tr>
                        <th>Agent Name</th>
                        <th>Properties for Sale</th>                        
                    </tr>
                    {items.map(item => (
                        <tr key={item.estateAgentName}>
                            <td>{item.estateAgentName}</td>
                            <td>{item.propertyCount}</td>
                        </tr>
                    ))}
                </tbody></table>
            ) || ""} 
            <button onClick={onClick}>Load listings</button>
    </div>
)

export default AgentListContainer;