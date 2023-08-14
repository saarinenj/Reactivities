import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Button, Header, List } from 'semantic-ui-react';

function App() {
  // activities is variable to store activities
  // setActivities is a function to call
  const [activities, setActivities] = useState([]);

  useEffect(() => {
    axios.get('http://localhost:5000/api/activities')
      .then(response => {
        setActivities(response.data);

      })
  }, []) // the [] brackets are needed to prevent client from requesting list of activities in a loop forever

  return (
    <div>
      <Header as='h2' icon='users' content='Reactivities' />

      <List>
        {activities.map((activity: any) => (
          <List.Item key={activity.id}>
            {activity.title}
          </List.Item>
        ))}
      </List>
      <Button content='test' />

    </div>
  );
}

export default App;
