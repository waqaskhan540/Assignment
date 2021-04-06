import AgentListContainer from "./Components/AgentListContainer"
import React, { Component } from "react"
import config from "./config"
import axios from "axios"

class App extends Component {

  constructor(props) {
    super(props)
    this.state = {
      topAgentsBySales: [],
      topAgentsByPropertyTypes: [],

      topAgentsBySalesLoading: false,
      topAgentsByPropertyTypesLoading: false,

      topAgentsBySalesError: null,
      topAgentsByPropertyTypesError: null
    }

    this.getTopAgentsBySales = this.getTopAgentsBySales.bind(this);
    this.getTopAgentsByPropertyTypes = this.getTopAgentsByPropertyTypes.bind(this);
  }

  getTopAgentsBySales() {


    let url = `${config.apiUrl}/api/agents/top/by-property-count`;

    this.setState({ topAgentsBySalesLoading: true })
    axios.get(url)
      .then(response => {
        this.setState({
          topAgentsBySales: response.data,
          topAgentsBySalesLoading: false,
          topAgentsBySalesError: null
        })
      })
      .catch(err => {
        let errorMessage = "Error fetching listings";
        if (err.response) {
          errorMessage = err.response.data.Error;
        }
        this.setState({ topAgentsBySalesLoading: false, topAgentsBySalesError: errorMessage })
      })
  }

  getTopAgentsByPropertyTypes() {
    let url = `${config.apiUrl}/api/agents/top/by-property-keyword`;

    this.setState({ topAgentsByPropertyTypesLoading: true })
    axios.get(url)
      .then(response => {
        this.setState({
          topAgentsByPropertyTypes: response.data,
          topAgentsByPropertyTypesLoading: false,
          topAgentsByPropertyTypesError: null
        })
      })
      .catch(err => {
        let errorMessage = "Error fetching listings";
        if (err.response) {
          errorMessage = err.response.data.Error;
        }
        this.setState({ topAgentsByPropertyTypesLoading: false, topAgentsByPropertyTypesError: errorMessage })
      })
  }

  render() {
    return (
      <div className="container">
        <div className="left">
          <AgentListContainer
            heading={"Agents in Amsterdam with most properties listed for sale."}
            items={this.state.topAgentsBySales}
            loading={this.state.topAgentsBySalesLoading}
            error={this.state.topAgentsBySalesError}
            onClick={this.getTopAgentsBySales}
          />
        </div>
        <div className="right">
          <AgentListContainer
            heading={"Top Agents selling properties with tuin"}
            items={this.state.topAgentsByPropertyTypes}
            loading={this.state.topAgentsByPropertyTypesLoading}
            error={this.state.topAgentsByPropertyTypesError}
            onClick={this.getTopAgentsByPropertyTypes}
          />
        </div>



      </div>
    )
  }
}

export default App;
