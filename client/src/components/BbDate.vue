<script lang="ts">
import formatDate from "date-fns/format";
import formatRelative from "date-fns/formatRelative";
import Vue, { VNodeData } from "vue";

const defaults = {
  dateFormat: "PPP",
  dateTimeFormat: "p EEEE, MMMM do",
  tooltipFormat: "PPpp"
};

interface Props {
  value: Date | string;
  format: string;
  tooltipFormat: string;
  dateTimeFormat: string;
  time: boolean;
}

const componentProps = {
  value: {
    type: [Date, String],
    required: true
  },

  format: {
    type: String,
    default: defaults.dateFormat
  },

  tooltipFormat: {
    type: String,
    default: defaults.tooltipFormat
  },

  dateTimeFormat: {
    type: String,
    default: defaults.dateTimeFormat
  },

  time: {
    type: Boolean,
    default: false
  },

  relative: {
    type: Boolean,
    default: false
  }
};

export default Vue.extend({
  functional: true,
  props: componentProps,

  render(h, context) {
    const { props, data } = context;
    const date = normalize(props.value);
    const formatters = determineFormatters(props);
    const text = props.relative ? formatRelative(date, new Date()) : formatDate(date, formatters.tooltip);
    const title = formatDate(date, formatters.text);
    const nodeData: VNodeData = {
      class: [data.staticClass],
      attrs: {
        title
      },
      domProps: {
        innerHTML: text
      }
    };
    return h("span", nodeData);
  }
});

const determineFormatters = (props: Props) => {
  const { time, format, tooltipFormat, dateTimeFormat } = props;

  return {
    text: time ? format : dateTimeFormat,
    tooltip: tooltipFormat
  };
};

const normalize = (value: Date | string) => {
  if (value instanceof Date) {
    return value;
  }
  return convertToDate(value);
};

const convertToDate = (value: string) => {
  return new Date(value);
};
</script>
