import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Button, List } from 'semantic-ui-react';
import { Activity } from '../models/activity';
import NavBar from './NavBar';

function App() {
  // activities is variable to store activities
  // setActivities is a function to call
  // note: specifying a Activity type here provides type safety and intellisense
  const [activities, setActivities] = useState<Activity[]>([]);

  useEffect(() => {
    // were expecting to get back an array of activities
    axios.get<Activity[]>('http://localhost:5000/api/activities')
      .then(response => {
        setActivities(response.data);

      })
  }, []) // the [] brackets are needed to prevent client from requesting list of activities in a loop forever

  return (
    <div>
      <NavBar />

      <List>
        {activities.map(activity => (
          <List.Item key={activity.id}>
            {activity.title}
          </List.Item>
        ))}
      </List>

    </div>
  );
}

export default App;
