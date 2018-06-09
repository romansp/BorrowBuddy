import axios from "axios";
import { IPayload, PayloadMapper, PayloadMessageTypes } from "../model";

export const server = axios.create({
  baseURL: "https://localhost:44347/api/",
  timeout: 1000
});

export function exec<T>(cb: Promise<{}>): Promise<T> {
  const onFulfilled = (value: {}) => new PayloadMapper().fromObject<T>(value);
  const onRejection = (reason: any) =>
    new PayloadMapper().fromObject<T>(reason);

  return cb.then(onFulfilled, onRejection).then(processPayload);
}

function processPayload<T>(
  payload: IPayload<T>,
  messageTypeIds?: string[]
): Promise<T> {
  const message = payload.message;
  messageTypeIds = messageTypeIds || [
    PayloadMessageTypes.error,
    PayloadMessageTypes.failure
  ];
  const messageTypeId = messageTypeIds.find(o => o === message.messageTypeId);

  if (messageTypeId) {
    return Promise.reject(message);
  }
  if (payload.data) {
    return Promise.resolve(payload.data);
  }
  throw new Error("Couldn't extract response data.");
}
