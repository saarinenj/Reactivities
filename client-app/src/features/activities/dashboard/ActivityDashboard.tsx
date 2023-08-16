import React from 'react';
import { Grid, List } from 'semantic-ui-react';
import { Activity } from '../../../app/models/activity';
import ActivityList from './ActivityList';
import ActivityDetails from '../details/ActivityDetails';
import ActivityForm from '../form/ActivityForm';

// publish list of activities in properties interface
interface Props {
    activities: Activity[];
}

export default function ActivityDashboard({activities}: Props) {
    return (
        <Grid>
            <Grid.Column width='10'>
                    <ActivityList activities={activities}/>
            </Grid.Column>
            <Grid.Column width='6'>
                { // making sure component is only loaded if there is an activity in index zero
                activities[0] && 
                <ActivityDetails activity={activities[0]}/>}
                <ActivityForm />
            </Grid.Column>
        </Grid>

    )

}