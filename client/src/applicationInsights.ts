import { AppInsights } from "applicationinsights-js";
import Vue, { VueConstructor } from "vue";
import VueRouter from "vue-router";

export interface ApplicationInsightsOptions {
  id: string;
  basePath: string;
  router: VueRouter;
}

export default function(_: VueConstructor<Vue>, options?: ApplicationInsightsOptions) {
  if (!options) return;

  const { id, router } = options;

  if (AppInsights.downloadAndSetup) {
    AppInsights.downloadAndSetup({ instrumentationKey: id });
    // Watch route event if router option is defined.
    if (router) {
      const pathFormatter = (path: string) => `${path.substr(1)}`;

      router.beforeEach((route, __, next) => {
        AppInsights.startTrackPage(route.fullPath);
        next();
      });

      router.afterEach(route => {
        AppInsights.stopTrackPage(pathFormatter(route.fullPath), route.fullPath);
      });
    }
  }
}
