import { Participant, ParticipantPost } from "@/shared/models";
import { exec, server } from "./api";

export function getAll() {
  return exec<Participant[]>(server.get("/participants"));
}

export function get(id: string) {
  return exec<Participant>(server.get(`/participants/${id}`));
}

export function add(participant: ParticipantPost) {
  return exec<Participant>(server.post("/participants", participant));
}

export function update(id: string, participant: Participant) {
  return exec<Participant>(server.put(`/participants/${id}`, participant));
}

export function remove(id: string) {
  return exec(server.delete(`/participants/${id}`));
}

export function balance(from: string, to: string, code: string) {
  return exec<number>(server.get(`/participants/${from}/balance/${to}/${code}`));
}
