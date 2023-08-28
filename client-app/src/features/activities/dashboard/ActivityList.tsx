import { Item, Segment } from 'semantic-ui-react';
import { useStore } from '../../../app/stores/store';
import { observer } from 'mobx-react-lite';
import ActivityListItem from './ActivityListItem';
import React from 'react';

export default observer(function ActivityList() {

    const {activityStore} = useStore();
    const {activitiesByDate} = activityStore;

    return (
        <Segment>
            <Item.Group divided>
                {activitiesByDate.map(activity => (
                   <ActivityListItem key={activity.id} activity={activity}></ActivityListItem>
                ))}
            </Item.Group>
        </Segment>
    )
})