import { createStore, compose, applyMiddleware } from 'redux'
import RootReducer from '../Reducers/root.reducer'
import thunkMiddleware from 'redux-thunk'
import history from '../history'

const configureStore = () => {
  let middleware = applyMiddleware(thunkMiddleware)

  if (process.env.NODE_ENV !== 'production') {
    const devToolsExtension = window.devToolsExtension
    if (typeof devToolsExtension === 'function') {
      middleware = compose(middleware, devToolsExtension())
    }
  }

  const store = createStore(RootReducer, undefined, middleware)

  if (module.hot) {
    module.hot.accept(RootReducer, () => {
      // Webpack 1.0 without router-redux bind: store.replaceReducer(require('./RootReducer').default);
      // Webpack 2.0 with router-redux bind:  store.replaceReducer(rootReducer(history))
      // Webpack 1.0 with router-redux bind (as below):
      const nextRootReducer = require(RootReducer).default
      store.replaceReducer(nextRootReducer(history))
    })
  }

  return store
}

export default configureStore
