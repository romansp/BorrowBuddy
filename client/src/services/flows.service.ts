import { Flow, FlowPost } from "@/shared/models";
import { exec, server } from "./api";

export function getAll() {
  return exec<Flow[]>(server.get("/flows"));
}

export function get(id: string) {
  return exec<Flow>(server.get(`/flows/${id}`));
}

export function add(flow: FlowPost) {
  return exec<Flow>(server.post("/flows", flow));
}

export function update(id: string, flow: Flow) {
  return exec<Flow>(server.put(`/flows/${id}`, flow));
}

export function remove(id: string) {
  return exec(server.delete(`/flows/${id}`));
}
