import { useEffect } from 'react';
import { Container } from 'semantic-ui-react';
import NavBar from './NavBar';
import ActivityDashboard from '../../features/activities/dashboard/ActivityDashboard';
import LoadingComponent from './LoadingComponent';
import { observer } from 'mobx-react-lite';
import { useStore } from '../stores/store';
import { Outlet } from 'react-router-dom';

function App() {


  return (
    <>
      <NavBar />
      <Container style={{ marginTop: '7em' }}>
      <Outlet />
      </Container>
    </>
  );
}

export default observer(App);
