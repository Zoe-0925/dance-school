import { hot } from 'react-hot-loader/root'
import React from 'react'
/**     Router    */
import { Router } from 'react-router-dom'
import { Switch, Route } from 'react-router-dom'
/**    Pages     */
import Admin from './Pages/Admin'
import Auth from './Pages/Auth'
import Student from './Pages/Student'
import './assets/stylesheets/main.scss'
import history from './history'
import ErrorBoundary from './ErrorBoundary'

const App = () => {
  return (
    <div className='App'>
      <ErrorBoundary>
        <Router history={history}>
          <Switch>
            <Route path='/' exact component={Auth} />
            <Route path='/auth' exact component={Auth} />
            <Route path='/admin' exact component={Admin} />
            <Route path='/student' exact component={Student} />
          </Switch>
        </Router>
      </ErrorBoundary>
    </div>
  )
}

export default process.env.NODE_ENV === 'development' ? hot(App) : App
